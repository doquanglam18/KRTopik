using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.UserDtos
{
    public class UserDetailDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? AvatarImageUrl { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NumberOfContributions { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
