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

}
