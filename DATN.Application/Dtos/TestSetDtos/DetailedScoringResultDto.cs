using System;
using System.Collections.Generic;

namespace DATN.Application.Dtos.TestSetDtos
{
    public class DetailedScoringResultDto
    {
        public int TotalQuestions { get; set; }
        public int CorrectCount { get; set; }
        public double ScorePercentage { get; set; }
        public List<QuestionResultDto> QuestionResults { get; set; } = new();
    }

    public class QuestionResultDto
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string QuestionType { get; set; } // "Listening" or "Reading"
        public string QuestionImageUrl { get; set; } // For reading questions
        public string QuestionAudioUrl { get; set; } // For listening questions
        public int? UserSelectedAnswerId { get; set; }
        public string UserSelectedAnswer { get; set; }
        public int CorrectAnswerId { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public List<AnswerOptionDto> AnswerOptions { get; set; } = new();
    }

    public class AnswerOptionDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
} 