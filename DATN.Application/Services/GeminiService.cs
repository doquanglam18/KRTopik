using DATN.Application.Dtos.GeminiDtos;
using DATN.Application.Dtos.GeminiDtos.RequestDtos;
using DATN.Application.Dtos.GeminiDtos.ResponseDtos;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiSettings _settings;
        private readonly IMemoryCache _cache;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private const string CACHE_KEY_PREFIX = "gemini_chat_";
        private const int MAX_PROMPT_LENGTH = 1000;
        private const int MAX_RETRY_ATTEMPTS = 3;
        private const int REQUEST_TIMEOUT_SECONDS = 30;
        private const string MODEL_NAME = "gemini-2.0-flash";  // Updated to latest model
        private const string API_VERSION = "v1beta";  // Updated API version
        private const int CACHE_DURATION_MINUTES = 60; // Cache duration for chat history

        public GeminiService(
            HttpClient httpClient, 
            IOptions<GeminiSettings> options,
            IMemoryCache cache)
        {
            _httpClient = httpClient;
            _settings = options.Value;
            _cache = cache;

            // Configure retry policy
            _retryPolicy = Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .Or<TimeoutException>()
                .OrResult(r => r.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                    MAX_RETRY_ATTEMPTS,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        // Log retry attempt
                        Console.WriteLine($"Retry attempt {retryCount} after {timeSpan.TotalSeconds}s due to {exception.GetType().Name}");
                    }
                );

            // Configure HttpClient timeout only
            _httpClient.Timeout = TimeSpan.FromSeconds(REQUEST_TIMEOUT_SECONDS);
        }

        public async Task<List<string>> ListAvailableModels()
        {
            try
            {
                var url = $"https://generativelanguage.googleapis.com/{API_VERSION}/models?key={_settings.ApiKey}";
                Console.WriteLine($"Listing models from: {url}");

                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"List models response: {content}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to list models: {content}");
                }

                var models = JsonConvert.DeserializeObject<ModelsResponse>(content);
                return models?.models?.Select(m => m.name).ToList() ?? new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing models: {ex.Message}");
                throw new GeminiServiceException("Failed to list available models", ex);
            }
        }

        private string GetCacheKey(string sessionId, string userId)
        {
            // Include userId in cache key to ensure separation between users
            return $"{CACHE_KEY_PREFIX}{userId}_{sessionId}";
        }

        public async Task<string> GetResponseFromGemini(string userInput, string sessionId, string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentException("User ID is required");

                // Validate input
                if (string.IsNullOrWhiteSpace(userInput))
                    throw new ArgumentException("Prompt cannot be empty");

                if (userInput.Length > MAX_PROMPT_LENGTH)
                    throw new ArgumentException($"Prompt length cannot exceed {MAX_PROMPT_LENGTH} characters");

                // Get or create conversation history with user ID
                var conversation = await GetOrCreateConversation(sessionId, userId);
                conversation.AddUserMessage(userInput);

                // Prepare request with conversation history
                var request = new GeminiRequest
                {
                    contents = conversation.Messages.Select(m => new Dtos.GeminiDtos.RequestDtos.Content
                    {
                        role = m.Role,
                        parts = new List<Dtos.GeminiDtos.RequestDtos.Part> { new Dtos.GeminiDtos.RequestDtos.Part { text = m.Text } }
                    }).ToList()
                };

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Use the correct model name and API version in the endpoint
                var endpointWithKey = $"https://generativelanguage.googleapis.com/{API_VERSION}/models/{MODEL_NAME}:generateContent?key={_settings.ApiKey}";
                Console.WriteLine($"Calling Gemini API at: {endpointWithKey}");
                Console.WriteLine($"Request body: {json}");

                // Execute request with retry policy
                var response = await _retryPolicy.ExecuteAsync(async () =>
                {
                    try
                    {
                        // Remove any existing Authorization header
                        _httpClient.DefaultRequestHeaders.Remove("Authorization");
                        
                        var result = await _httpClient.PostAsync(endpointWithKey, content);
                        var responseContent = await result.Content.ReadAsStringAsync();
                        Console.WriteLine($"Response status: {result.StatusCode}");
                        Console.WriteLine($"Response content: {responseContent}");
                        
                        if (!result.IsSuccessStatusCode)
                        {
                            throw new HttpRequestException($"API call failed with status {result.StatusCode}: {responseContent}");
                        }
                        
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in API call: {ex.Message}");
                        throw;
                    }
                });

                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Parsing response: {responseString}");
                
                var geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(responseString);

                if (geminiResponse?.candidates == null || !geminiResponse.candidates.Any())
                {
                    Console.WriteLine("Invalid response: No candidates in response");
                    throw new InvalidOperationException("Invalid response from Gemini API: No candidates");
                }

                var responseText = geminiResponse.candidates[0]?.content?.parts?[0]?.text;
                if (string.IsNullOrWhiteSpace(responseText))
                {
                    Console.WriteLine("Invalid response: Empty text in candidate");
                    throw new InvalidOperationException("Empty response from Gemini API");
                }

                // Add response to conversation history
                conversation.AddAssistantMessage(responseText);
                await UpdateConversationCache(sessionId, conversation, userId);

                return responseText;
            }
            catch (Exception ex)
            {
                // Log the error with more details
                Console.WriteLine($"Error in GetResponseFromGemini: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner exception stack trace: {ex.InnerException.StackTrace}");
                }
                throw new GeminiServiceException("Failed to get response from Gemini", ex);
            }
        }

        public void ClearChatHistory(string sessionId, string userId)
        {
            if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(userId))
                return;

            var cacheKey = GetCacheKey(sessionId, userId);
            _cache.Remove(cacheKey);
            Console.WriteLine($"Cleared chat history for user {userId}, session: {sessionId}");
        }

        public void ClearUserChatHistories(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return;

            var cacheEntries = _cache.GetType()
                .GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(_cache) as dynamic;

            if (cacheEntries != null)
            {
                foreach (var cacheEntry in cacheEntries)
                {
                    var key = cacheEntry.GetType().GetProperty("Key")?.GetValue(cacheEntry) as string;
                    if (key?.StartsWith($"{CACHE_KEY_PREFIX}{userId}_") == true)
                    {
                        _cache.Remove(key);
                        Console.WriteLine($"Cleared chat history for user {userId}, key: {key}");
                    }
                }
            }
        }

        private async Task<Conversation> GetOrCreateConversation(string sessionId, string userId)
        {
            var cacheKey = GetCacheKey(sessionId, userId);
            return await _cache.GetOrCreateAsync(cacheKey, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(CACHE_DURATION_MINUTES);
                entry.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    Console.WriteLine($"Chat history for user {userId}, session {sessionId} was removed. Reason: {reason}");
                });
                return Task.FromResult(new Conversation());
            });
        }

        private async Task UpdateConversationCache(string sessionId, Conversation conversation, string userId)
        {
            var cacheKey = GetCacheKey(sessionId, userId);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(CACHE_DURATION_MINUTES))
                .RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    Console.WriteLine($"Chat history for user {userId}, session {sessionId} was removed. Reason: {reason}");
                });
            
            _cache.Set(cacheKey, conversation, cacheEntryOptions);
        }
    }

    public class Conversation
    {
        public List<Message> Messages { get; } = new List<Message>();
        private const int MAX_MESSAGES = 10;

        public void AddUserMessage(string text)
        {
            AddMessage("user", text);
        }

        public void AddAssistantMessage(string text)
        {
            AddMessage("assistant", text);
        }

        private void AddMessage(string role, string text)
        {
            Messages.Add(new Message { Role = role, Text = text });
            
            // Keep only the last MAX_MESSAGES messages
            while (Messages.Count > MAX_MESSAGES)
            {
                Messages.RemoveAt(0);
            }
        }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Text { get; set; }
    }

    public class GeminiServiceException : Exception
    {
        public GeminiServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class ModelsResponse
    {
        public List<ModelInfo> models { get; set; }
    }

    public class ModelInfo
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public List<string> supportedGenerationMethods { get; set; }
    }
}
