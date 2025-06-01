using DATN.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(nameof(Comment));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.HasOne(x => x.User).WithMany(c => c.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.TestSet).WithMany(c => c.Comments).HasForeignKey(x => x.TestSetId).OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Comment
                {
                    Id = 1,
                    UserId = new Guid("5C0C563B-80D4-4485-9854-F6AF58422601"),
                    TestSetId = 1,
                    Content = "Đề hay, nội dung ok !",
                    Rating = 5,
                    CreatedDate = DateTime.Now
                },
                new Comment
                {
                    Id = 2,
                    UserId = new Guid("5C0C563B-80D4-4485-9854-F6AF58422601"),
                    TestSetId = 2,
                    Content = "Đề hay, nội dung ok !",
                    CreatedDate = DateTime.Now,
                    Rating = 5
                },
                new Comment
                {
                    Id = 6,
                    UserId = new Guid("5C0C563B-80D4-4485-9854-F6AF58422601"),
                    TestSetId = 3,
                    Content = "Đề tạm được, khá sát !",
                    CreatedDate = DateTime.Now,
                    Rating = 4
                },
                new Comment
                {
                    Id = 3,
                    UserId = new Guid("EA81763F-6534-448E-AA30-4112123493FB"),
                    TestSetId = 1,
                    Content = "Đề hay, nội dung hay !",
                    CreatedDate = DateTime.Now,
                    Rating = 5
                },
                new Comment
                {
                    Id =4,
                    UserId = new Guid("EA81763F-6534-448E-AA30-4112123493FB"),
                    TestSetId = 2,
                    Content = "Cần phải bổ sung nhiều",
                    CreatedDate = DateTime.Now,
                    Rating = 3
                },
                new Comment
                {
                    Id = 5,
                    UserId = new Guid("EA81763F-6534-448E-AA30-4112123493FB"),
                    TestSetId = 3,
                    Content = "Đề cơ bản !",
                    CreatedDate = DateTime.Now,
                    Rating = 3
                }

            );
        }
    }
}
