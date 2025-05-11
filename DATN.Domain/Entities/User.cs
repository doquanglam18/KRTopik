using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class User
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
        public Role Role { get; set; }
        public string? VerificationToken { get; set; }

        public List<SystemLogging>? SystemLoggings { get; set; }

        public List<RatingBlog>? RatingBlogs { get; set; }

        public List<UserProgress>? UserProgresses { get; set; }

        public List<Comment> Comments { get; set; }
        public bool IsActive { get; set; }
    }
}
