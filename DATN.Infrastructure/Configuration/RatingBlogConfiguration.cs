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
    public class RatingBlogConfiguration : IEntityTypeConfiguration<RatingBlog>
    {
        public void Configure(EntityTypeBuilder<RatingBlog> builder)
        {
            builder.ToTable("RatingBlog");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Rating).IsRequired();
            builder.HasCheckConstraint("CK_KoreaBlog_Rating", "[Rating] >= 1 AND [Rating] <= 5");
            builder.Property(x => x.Content).IsRequired();
            builder.HasOne(x => x.User).WithMany(c => c.RatingBlogs).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.KoreaBlog).WithMany(c => c.RatingBlogs).HasForeignKey(x => x.BlogId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
