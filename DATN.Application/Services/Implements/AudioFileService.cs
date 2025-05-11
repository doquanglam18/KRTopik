/*using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class AudioFileService : IAudioFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Cloudinary _cloudinary;

        public AudioFileService(IUnitOfWork unitOfWork, Cloudinary cloudinary)
        {
            _unitOfWork = unitOfWork;
            _cloudinary = cloudinary;
        }

        public async Task<Result> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Result.Failure("File không hợp lệ.");
            }

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = "audio_files"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return Result.Failure("Tải file lên thất bại.");
            }

            var audioFile = new AudioFile
            {
                FileName = file.FileName,
                FileUrl = uploadResult.SecureUrl.ToString(),
                ContentType = file.ContentType,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _unitOfWork.AudioFileRepository.Add(audioFile);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success("Tải file thành công.");
        }

        public async Task<ResultV<AudioFileDto>> DownloadFileAsync(int id)
        {
            var file = await _unitOfWork.AudioFileRepository.GetByIdAsync(id);
            if (file == null)
            {
                return ResultV<AudioFileDto>.Failure("Không tìm thấy file.");
            }

            var dto = new AudioFileDto
            {
                Id = file.Id,
                FileName = file.FileName,
                FileUrl = file.FileUrl,
                ContentType = file.ContentType
            };

            return ResultV<AudioFileDto>.Success(dto);
        }

        public async Task<List<AudioFileDto>> GetAllFileAsync()
        {
            var files = await _unitOfWork.AudioFileRepository.GetAll().ToListAsync();
            return files.Select(f => new AudioFileDto
            {
                Id = f.Id,
                FileName = f.FileName,
                FileUrl = f.FileUrl,
                ContentType = f.ContentType
            }).ToList();
        }
    }
}

*/