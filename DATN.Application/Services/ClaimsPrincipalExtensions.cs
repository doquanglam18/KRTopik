using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
        }

        public static string ExtractPublicIdFromUrl(string url)
        {
            // Bỏ domain, version và extension
            try
            {
                var uri = new Uri(url);
                var segments = uri.AbsolutePath.Split('/'); // /demo/video/upload/v1234567/folder/audio_sample.mp3
                var uploadIndex = Array.IndexOf(segments, "upload");

                if (uploadIndex < 0 || uploadIndex + 1 >= segments.Length)
                    return null;

                // Lấy phần từ sau "upload"
                var publicPathSegments = segments.Skip(uploadIndex + 1).ToArray();
                var filename = publicPathSegments[^1];

                // Loại bỏ đuôi .mp3, .wav, v.v.
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
                publicPathSegments[^1] = fileNameWithoutExtension;

                return string.Join("/", publicPathSegments);
            }
            catch
            {
                return null;
            }
        }

    }

}
