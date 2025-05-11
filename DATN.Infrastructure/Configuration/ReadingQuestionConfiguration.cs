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
    public class ReadingQuestionConfiguration : IEntityTypeConfiguration<ReadingQuestion>
    {
        public void Configure(EntityTypeBuilder<ReadingQuestion> builder)
        {
            builder.ToTable(nameof(ReadingQuestion));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Question).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.IsPublic).IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.TestSet).WithMany(c => c.ReadingQuestions).HasForeignKey(x => x.TestSetId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.RankQuestion).WithMany(c => c.ReadingQuestions).HasForeignKey(x => x.RankQuestionId).OnDelete(DeleteBehavior.NoAction);


            // Seed dữ liệu mẫu
            builder.HasData(
                new ReadingQuestion
                {
                    Id = 1,
                    Question = "(   )에 들어갈 가장 알맞은 것을 고르십시오. <br>" +
                    "나는 오래전에 설악산을 (       ).",
                    ReadingImageURL = null,
                    CreatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    UpdatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    RankQuestionId = 1,
                    IsPublic = true
                },
                new ReadingQuestion
                {
                    Id = 2,
                    Question = "(   )에 들어갈 가장 알맞은 것을 고르십시오. <br>" +
                    "새집으로 (       ) 가구를 새로 샀다.",
                    ReadingImageURL = null,
                    CreatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    UpdatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    RankQuestionId = 1,
                    IsPublic = true
                },
                new ReadingQuestion
                {
                    Id = 3,
                    Question = "다음 밑줄 친 부분과 의미가 비슷한 것을 고르십시오.<br>" + "어려운 이웃을 <u>돕고자</u> 매년 봉사 활동에 참여하고 있다.",
                    ReadingImageURL = null,
                    CreatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    UpdatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    RankQuestionId = 1,
                    IsPublic = true
                },

                new ReadingQuestion
                {
                    Id = 4,
                    Question = "다음 밑줄 친 부분과 의미가 비슷한 것을 고르십시오.<br>" + "지난 3년 동안 영화를 한 편 봤으니 거의 안 본 <u>셈이다</u>.",
                    ReadingImageURL = null,
                    CreatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    UpdatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    RankQuestionId = 1,
                    IsPublic = true
                }
            );
        }

    }
}
