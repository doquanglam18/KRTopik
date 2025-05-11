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
    public class ListeningAnswerConfiguration : IEntityTypeConfiguration<ListeningAnswer>
    {
        public void Configure(EntityTypeBuilder<ListeningAnswer> builder)
        {
            builder.ToTable(nameof(ListeningAnswer));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.IsCorrect).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
           builder.HasOne(x => x.ListeningQuestion).WithMany(c => c.ListeningAnswers).HasForeignKey(x => x.ListeningQuestionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
