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
    public class RatingBlogConfiguration : IEntityTypeConfiguration<RatingBlog>
    {
        public void Configure(EntityTypeBuilder<RatingBlog> builder)
        {
            builder.ToTable("RatingBlog");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Rating).IsRequired();
            builder.HasCheckConstraint("CK_KoreaBlog_Rating", "[Rating] >= 1 AND [Rating] <= 5");
            builder.HasOne(x => x.User).WithMany(c => c.RatingBlogs).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.KoreaBlog).WithMany(c => c.RatingBlogs).HasForeignKey(x => x.BlogId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(
                new RatingBlog
                {
                    Id = 1,
                    UserId = new Guid("5C0C563B-80D4-4485-9854-F6AF58422601"),
                    BlogId = 1,
                    Content = "Không có ảnh à ?",
                    CreatedDate = DateTime.Now,
                    Rating = 5
                },
                 new RatingBlog
                 {
                     Id = 2,
                     UserId = new Guid("5C0C563B-80D4-4485-9854-F6AF58422601"),
                     BlogId = 2,
                     Content = "Bài viết hay, ok",
                     CreatedDate = DateTime.Now,
                     Rating = 4
                 },
                  new RatingBlog
                  {Id = 3,
                      UserId = new Guid("5C0C563B-80D4-4485-9854-F6AF58422601"),
                      BlogId = 3,
                      Content = "Rất hợp lý",
                      CreatedDate = DateTime.Now,
                      Rating = 2
                  },
                   new RatingBlog
                   {
                       Id = 4,
                       UserId = new Guid("5C0C563B-80D4-4485-9854-F6AF58422601"),
                       BlogId = 4,
                       Content = "Hot đấy",
                       CreatedDate = DateTime.Now,
                       Rating = 5
                   },
                    new RatingBlog
                    {
                        Id = 5,
                        UserId = new Guid("EA81763F-6534-448E-AA30-4112123493FB"),
                        BlogId = 1,
                        Content = "Không có ảnh à ?",
                        CreatedDate = DateTime.Now,
                        Rating = 3
                    },
                 new RatingBlog
                 {
                     Id = 6,
                     UserId = new Guid("EA81763F-6534-448E-AA30-4112123493FB"),
                     BlogId = 2,
                     Content = "Bài viết hay, ok",
                     CreatedDate = DateTime.Now,
                     Rating = 2
                 },
                  new RatingBlog
                  {
                      Id = 7,
                      UserId = new Guid("EA81763F-6534-448E-AA30-4112123493FB"),
                      BlogId = 3,
                      Content = "Rất hợp lý",
                      CreatedDate = DateTime.Now,
                      Rating = 5
                  },
                   new RatingBlog
                   {
                       Id = 8,
                       UserId = new Guid("EA81763F-6534-448E-AA30-4112123493FB"),
                       BlogId = 4,
                       Content = "Hot đấy",
                       CreatedDate = DateTime.Now,
                       Rating = 5
                   }

            );
        }
    }
}
