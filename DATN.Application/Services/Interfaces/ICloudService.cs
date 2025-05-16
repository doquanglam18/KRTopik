using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface ICloudService
    {
        Task<string> UploadImageAsync(IFormFile imageFile); // Phương thức tải ảnh lên Cloud
        Task<byte[]> DownloadImageAsync(string publicId); // Phương thức tải ảnh từ Cloud

        Task<string> UploadAudioAsync(IFormFile audioFile);
        Task<byte[]> DownloadAudioAsync(string publicId);

        Task<double?> GetAudioDurationViaApiAsync(string publicId);
    }

}
