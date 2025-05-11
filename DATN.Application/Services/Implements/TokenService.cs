using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class TokenService : ITokenService
    {
        public string GenerateVerificationToken(User user)
        {
            // Token đơn giản ngẫu nhiên, có thể dùng JWT hoặc Guid tùy thích
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            user.VerificationToken = token;
            return token;
        }
    }

}
