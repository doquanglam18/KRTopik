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
    public class ReadingAnswerConfiguration : IEntityTypeConfiguration<ReadingAnswer>
    {
        public void Configure(EntityTypeBuilder<ReadingAnswer> builder)
        {
            builder.ToTable(nameof(ReadingAnswer));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.IsCorrect).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.HasOne(x => x.ReadingQuestion).WithMany(c => c.ReadingAnswers).HasForeignKey(x => x.ReadingQuestionId).OnDelete(DeleteBehavior.Cascade);

            // Dữ liệu mẫu
            builder.HasData(
                new ReadingAnswer { Id = 1, ReadingQuestionId = 1, Content = "등산하고 싶다", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 2, ReadingQuestionId = 1, Content = "등산해도 된다 ", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 3, ReadingQuestionId = 1, Content = "등산할 것 같다", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 4, ReadingQuestionId = 1, Content = "등산한 적이 있다", IsCorrect = true, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 5, ReadingQuestionId = 2, Content = "이사한 지", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 6, ReadingQuestionId = 2, Content = "이사하거든", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 7, ReadingQuestionId = 2, Content = "이사하려면 ", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 8, ReadingQuestionId = 2, Content = "이사하고 나서 ", IsCorrect = true, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 9, ReadingQuestionId = 3, Content = "돕기 위해서", IsCorrect = true, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 10, ReadingQuestionId = 3, Content = "돕는 대신에", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 11, ReadingQuestionId = 3, Content = "돕기 무섭게", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 12, ReadingQuestionId = 3, Content = "돕는 바람에 ", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 13, ReadingQuestionId = 4, Content = "본 척했다", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 14, ReadingQuestionId = 4, Content = "보기 나름이다", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 15, ReadingQuestionId = 4, Content = "보기 나름이다", IsCorrect = false, CreatedDate = DateTime.Now },
                new ReadingAnswer { Id = 16, ReadingQuestionId = 4, Content = "본 거나 마찬가지이다", IsCorrect = true, CreatedDate = DateTime.Now }
            );
        }


    }
}
