using DATN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(85);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(85);
            builder.Property(x => x.AvatarImageUrl).IsRequired(false).HasDefaultValue("https://res.cloudinary.com/dmsi8fr0l/image/upload/v1746432150/user_hbigun.png");
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.RoleId).IsRequired().HasDefaultValue(3);
            builder.Property(x => x.NumberOfContributions).IsRequired().HasDefaultValue(0);
            builder.HasOne(x => x.Role).WithMany(c => c.Users).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.NoAction);

            var a = BCrypt.Net.BCrypt.HashPassword("a");

            var admin = BCrypt.Net.BCrypt.HashPassword("admin");

            builder.HasData(
                new User
                {
                    Id = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    FullName = "Đỗ Quang Lâm",
                    Email = "dolam180903@gmail.com",
                    PasswordHash = a, // "a"
                    DateOfBirth = new DateTime(2003, 09, 18),
                    NumberOfContributions = 5,
                    CreatedDate = new DateTime(2024, 4, 1),
                    UpdatedDate = new DateTime(2024, 4, 10),
                    RoleId = 2,
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("ea81763f-6534-448e-aa30-4112123493fb"),
                    FullName = "Trần Thị B",
                    Email = "b@gmail.com",
                    PasswordHash = a, // "a"
                    DateOfBirth = new DateTime(1998, 5, 20),
                    NumberOfContributions = 0,
                    CreatedDate = new DateTime(2024, 4, 5),
                    UpdatedDate = new DateTime(2024, 4, 10),
                    RoleId = 3,
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    FullName = "System Admin",
                    Email = "admin@gmail.com",
                    PasswordHash = admin, // "admin"
                    DateOfBirth = new DateTime(1990, 9, 15),
                    NumberOfContributions = 12,
                    CreatedDate = new DateTime(2024, 4, 8),
                    UpdatedDate = new DateTime(2024, 4, 10),
                    RoleId = 1,
                    IsActive = true
                }
            );

        }
    }
}
