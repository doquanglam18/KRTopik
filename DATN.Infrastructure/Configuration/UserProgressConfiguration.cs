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
    public class UserProgressConfiguration : IEntityTypeConfiguration<UserProgress>
    {
        public void Configure(EntityTypeBuilder<UserProgress> builder)
        {
            builder.ToTable(nameof(UserProgress));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstAttemptAt).IsRequired();
            builder.Property(x => x.BestResults).IsRequired();
            builder.Property(x => x.TotalQuestions).IsRequired();
            builder.Property(x => x.CompletedQuestions).IsRequired().HasDefaultValue(0);
            builder.HasOne(x => x.User).WithMany(c => c.UserProgresses).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.TestSet).WithMany(c => c.UserProgress).HasForeignKey(x => x.TestSetId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
