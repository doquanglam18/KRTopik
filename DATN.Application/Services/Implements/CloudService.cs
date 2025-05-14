using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DATN.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;


public class CloudService : ICloudService
{
    private readonly Cloudinary _cloudinary;

    // Constructor nhận thông tin cấu hình từ appsettings.json
    public CloudService(IConfiguration configuration)
    {
        var cloudName = configuration["CloudinarySettings:CloudName"];
        var apiKey = configuration["CloudinarySettings:ApiKey"];
        var apiSecret = configuration["CloudinarySettings:ApiSecret"];

        if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
        {
            throw new ArgumentException("Cloudinary configuration is missing.");
        }

        var account = new Account(cloudName, apiKey, apiSecret);
        _cloudinary = new Cloudinary(account);
    }

    // Phương thức tải ảnh lên Cloud
    public async Task<string> UploadImageAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return null;

        // Tạo đối tượng upload params với file từ client
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream())
        };

        // Thực hiện upload ảnh lên Cloudinary
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        // Trả về URL ảnh sau khi upload thành công
        return uploadResult?.SecureUrl?.ToString();
    }

    public async Task<byte[]> DownloadImageAsync(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
            return null;

        // Lấy tài nguyên từ Cloudinary bằng PublicId
        var resourceParams = new GetResourceParams(publicId);
        var resource = await _cloudinary.GetResourceAsync(resourceParams);

        if (resource.StatusCode == HttpStatusCode.OK)
        {
            // Lấy URL của ảnh từ Cloudinary
            var imageUrl = resource.SecureUrl.ToString();

            // Tải ảnh về từ URL (sử dụng HttpClient)
            using (var client = new HttpClient())
            {
                var imageBytes = await client.GetByteArrayAsync(imageUrl);
                return imageBytes; // Trả về mảng byte của ảnh
            }
        }

        return null; // Nếu không tìm thấy tài nguyên
    }


    // Upload file âm thanh (mp3, wav, m4a,...)
    public async Task<string> UploadAudioAsync(IFormFile audioFile)
    {
        if (audioFile == null || audioFile.Length == 0)
            return null;

        var uploadParams = new RawUploadParams
        {
            File = new FileDescription(audioFile.FileName, audioFile.OpenReadStream())
            // KHÔNG gán ResourceType ở đây vì nó chỉ đọc được
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult?.SecureUrl?.ToString();
    }



    // Download file âm thanh bằng publicId
    public async Task<byte[]> DownloadAudioAsync(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
            return null;

        var resourceParams = new GetResourceParams(publicId)
        {
            ResourceType = ResourceType.Video // ✅ Dùng enum đúng kiểu
        };

        var resource = await _cloudinary.GetResourceAsync(resourceParams);

        if (resource.StatusCode == HttpStatusCode.OK)
        {
            var audioUrl = resource.SecureUrl.ToString();

            using (var client = new HttpClient())
            {
                return await client.GetByteArrayAsync(audioUrl);
            }
        }

        return null;
    }

}
