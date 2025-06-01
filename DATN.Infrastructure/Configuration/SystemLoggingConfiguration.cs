using DATN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Configuration
{
    public class SystemLoggingConfiguration : IEntityTypeConfiguration<SystemLogging>
    {
        public void Configure(EntityTypeBuilder<SystemLogging> builder)
        {
            builder.ToTable(nameof(SystemLogging));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IPAddress).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ActionName).IsRequired();
            builder.Property(x => x.Details).IsRequired();
            builder.HasOne(x => x.User).WithMany(c => c.SystemLoggings).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasData(
            new SystemLogging
            {
                Id = 1,
                UserId = Guid.Parse("EA81763F-6534-448E-AA30-4112123493FB"),
                IPAddress = "::1",
                ActionName = "Logout",
                Details = "Người dùng đã đăng xuất khỏi hệ thống",
                CreatedDate = new DateTime(2025, 5, 1, 8, 0, 0),
                UpdatedDate = DateTime.MinValue
            },
            new SystemLogging
            {
                Id = 2,
                UserId = Guid.Parse("5C0C563B-80D4-4485-9854-F6AF58422601"),
                IPAddress = "::1",
                ActionName = "Logout",
                Details = "Người dùng đã đăng xuất khỏi hệ thống",
                CreatedDate = new DateTime(2025, 5, 1, 13, 0, 0),
                UpdatedDate = DateTime.MinValue
            },
            new SystemLogging
            {
                Id = 3,
                UserId = null,
                IPAddress = "::1",
                ActionName = "Login - Failed",
                Details = "Email: dolam180903@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !",
                CreatedDate = new DateTime(2025, 5, 2, 11, 0, 0),
                UpdatedDate = DateTime.MinValue
            },
            new SystemLogging
            {
                Id = 4,
                UserId = null,
                IPAddress = "::1",
                ActionName = "Login - Failed",
                Details = "Email: admin@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !",
                CreatedDate = new DateTime(2025, 5, 3, 12, 0, 0),
                UpdatedDate = DateTime.MinValue
            },
            new SystemLogging
            {
                Id = 5,
                UserId = Guid.Parse("5C0C563B-80D4-4485-9854-F6AF58422601"),
                IPAddress = "::1",
                ActionName = "Login - Success",
                Details = "User Đỗ Quang Lâm (dolam180903@gmail.com) đã đăng nhập thành công.",
                CreatedDate = new DateTime(2025, 5, 4, 13, 0, 0),
                UpdatedDate = DateTime.MinValue
            },
            new SystemLogging
            {
                Id = 6,
                UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                IPAddress = "::1",
                ActionName = "Login - Success",
                Details = "User System Admin (admin@gmail.com) đã đăng nhập thành công.",
                CreatedDate = new DateTime(2025, 5, 5, 14, 0, 0),
                UpdatedDate = DateTime.MinValue
            },
new SystemLogging
{
    Id = 7,
    UserId = Guid.Parse("EA81763F-6534-448E-AA30-4112123493FB"),
    IPAddress = "::1",
    ActionName = "Logout",
    Details = "Người dùng đã đăng xuất khỏi hệ thống",
    CreatedDate = new DateTime(2025, 5, 6, 15, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 8,
    UserId = null,
    IPAddress = "::1",
    ActionName = "Login - Failed",
    Details = "Email: admin@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !",
    CreatedDate = new DateTime(2025, 5, 7, 9, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 9,
    UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User System Admin (admin@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 8, 10, 30, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 10,
    UserId = Guid.Parse("EA81763F-6534-448E-AA30-4112123493FB"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User Trần Thị B (b@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 9, 16, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 11,
    UserId = Guid.Parse("5C0C563B-80D4-4485-9854-F6AF58422601"),
    IPAddress = "::1",
    ActionName = "Logout",
    Details = "Người dùng đã đăng xuất khỏi hệ thống",
    CreatedDate = new DateTime(2025, 5, 10, 11, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 12,
    UserId = Guid.Parse("EA81763F-6534-448E-AA30-4112123493FB"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User Trần Thị B (b@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 11, 13, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 13,
    UserId = null,
    IPAddress = "::1",
    ActionName = "Login - Failed",
    Details = "Email: a@example.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !",
    CreatedDate = new DateTime(2025, 5, 12, 17, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 14,
    UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
    IPAddress = "::1",
    ActionName = "Logout",
    Details = "Người dùng đã đăng xuất khỏi hệ thống",
    CreatedDate = new DateTime(2025, 5, 13, 14, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 15,
    UserId = Guid.Parse("EA81763F-6534-448E-AA30-4112123493FB"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User Trần Thị B (b@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 14, 13, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 16,
    UserId = Guid.Parse("5C0C563B-80D4-4485-9854-F6AF58422601"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User Đỗ Quang Lâm (dolam180903@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 15, 9, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 17,
    UserId = null,
    IPAddress = "::1",
    ActionName = "Login - Failed",
    Details = "Email: dolam180903@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !",
    CreatedDate = new DateTime(2025, 5, 16, 8, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 18,
    UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User System Admin (admin@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 17, 15, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 19,
    UserId = Guid.Parse("5C0C563B-80D4-4485-9854-F6AF58422601"),
    IPAddress = "::1",
    ActionName = "Logout",
    Details = "Người dùng đã đăng xuất khỏi hệ thống",
    CreatedDate = new DateTime(2025, 5, 18, 16, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 20,
    UserId = null,
    IPAddress = "::1",
    ActionName = "Login - Failed",
    Details = "Email: admin@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !",
    CreatedDate = new DateTime(2025, 5, 19, 10, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 21,
    UserId = Guid.Parse("EA81763F-6534-448E-AA30-4112123493FB"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User Trần Thị B (b@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 20, 14, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 22,
    UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
    IPAddress = "::1",
    ActionName = "Logout",
    Details = "Người dùng đã đăng xuất khỏi hệ thống",
    CreatedDate = new DateTime(2025, 5, 21, 12, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 23,
    UserId = Guid.Parse("5C0C563B-80D4-4485-9854-F6AF58422601"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User Đỗ Quang Lâm (dolam180903@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 22, 8, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 24,
    UserId = Guid.Parse("EA81763F-6534-448E-AA30-4112123493FB"),
    IPAddress = "::1",
    ActionName = "Logout",
    Details = "Người dùng đã đăng xuất khỏi hệ thống",
    CreatedDate = new DateTime(2025, 5, 23, 10, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 25,
    UserId = null,
    IPAddress = "::1",
    ActionName = "Login - Failed",
    Details = "Email: a@example.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !",
    CreatedDate = new DateTime(2025, 5, 24, 11, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 26,
    UserId = Guid.Parse("5C0C563B-80D4-4485-9854-F6AF58422601"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User Đỗ Quang Lâm (dolam180903@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 25, 14, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 27,
    UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User System Admin (admin@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 26, 10, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 28,
    UserId = Guid.Parse("EA81763F-6534-448E-AA30-4112123493FB"),
    IPAddress = "::1",
    ActionName = "Logout",
    Details = "Người dùng đã đăng xuất khỏi hệ thống",
    CreatedDate = new DateTime(2025, 5, 27, 9, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 29,
    UserId = null,
    IPAddress = "::1",
    ActionName = "Login - Failed",
    Details = "Email: admin@gmail.com - Lý do: Tài khoản hoặc mật khẩu không chính xác !",
    CreatedDate = new DateTime(2025, 5, 28, 15, 0, 0),
    UpdatedDate = DateTime.MinValue
},
new SystemLogging
{
    Id = 30,
    UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
    IPAddress = "::1",
    ActionName = "Login - Success",
    Details = "User System Admin (admin@gmail.com) đã đăng nhập thành công.",
    CreatedDate = new DateTime(2025, 5, 29, 16, 0, 0),
    UpdatedDate = DateTime.MinValue
}

        );

        }
    }
}

