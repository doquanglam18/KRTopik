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
    public class ListeningQuestionConfiguration : IEntityTypeConfiguration<ListeningQuestion>
    {
        public void Configure(EntityTypeBuilder<ListeningQuestion> builder)
        {
            builder.ToTable(nameof(ListeningQuestion));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Question).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.IsPublic).IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.TestSet).WithMany(c => c.ListeningQuestions).HasForeignKey(x => x.TestSetId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.RankQuestion).WithMany(c => c.ListeningQuestions).HasForeignKey(x => x.RankQuestionId).OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
