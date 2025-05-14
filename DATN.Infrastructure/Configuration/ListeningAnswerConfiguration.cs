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

            builder.HasData(
                new ListeningAnswer
                {
                    Id = 1,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023773/C%C3%A2u_1_%C3%BD_1_luh7aq.png",
                    IsCorrect = true,
                    ListeningQuestionId = 1,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 2,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747039219/C%C3%A2u_1_%C3%BD_2_o6ifse.png",
                    IsCorrect = false,
                    ListeningQuestionId = 1,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 3,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747039219/C%C3%A2u_1_%C3%BD_3_vcw8yi.png",
                    IsCorrect = false,
                    ListeningQuestionId = 1,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 4,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_1_%C3%BD_4_s0ryfi.png",
                    IsCorrect = false,
                    ListeningQuestionId = 1,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 5,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_2_%C3%BD_1_msuecz.png",
                    IsCorrect = false,
                    ListeningQuestionId = 2,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 6,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023773/C%C3%A2u_2_%C3%BD_2_qgct0r.png",
                    IsCorrect = false,
                    ListeningQuestionId = 2,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 7,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_2_%C3%BD_3_bngzlb.png",
                    IsCorrect = true,
                    ListeningQuestionId = 2,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 8,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_2_%C3%BD_4_ewkro4.png",
                    IsCorrect = false,
                    ListeningQuestionId = 2,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 9,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_3_%C3%BD_1_v8melr.png",
                    IsCorrect = false,
                    ListeningQuestionId = 3,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 10,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_3_%C3%BD_2_jngpdj.png",
                    IsCorrect = false,
                    ListeningQuestionId = 3,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 11,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_3_%C3%BD_3_mr5euu.png",
                    IsCorrect = false,
                    ListeningQuestionId = 3,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 12,
                    Content = "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_3_%C3%BD_4_z6zjwj.png",
                    IsCorrect = true,
                    ListeningQuestionId = 3,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 13,
                    Content = "장소를 다시 말해 주세요",
                    IsCorrect = false,
                    ListeningQuestionId = 4,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 14,
                    Content = "다음 모임은 안 갈 거예요",
                    IsCorrect = false,
                    ListeningQuestionId = 4,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 15,
                    Content = "이번 주에 만나면 좋겠어요",
                    IsCorrect = false,
                    ListeningQuestionId = 4,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 16,
                    Content = "정문 옆에 있는 식당이에요",
                    IsCorrect = true,
                    ListeningQuestionId = 4,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 17,
                    Content = "아침 일찍 기차를 탔어",
                    IsCorrect = false,
                    ListeningQuestionId = 5,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 18,
                    Content = "표가 없어서 아직 못 갔어",
                    IsCorrect = false,
                    ListeningQuestionId = 5,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 19,
                    Content = "표가 있는지 한번 알아볼게",
                    IsCorrect = true,
                    ListeningQuestionId = 5,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 20,
                    Content = "금요일 오후 표는 취소하자",
                    IsCorrect = false,
                    ListeningQuestionId = 5,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 21,
                    Content = "발표는 늘 어렵지요",
                    IsCorrect = false,
                    ListeningQuestionId = 6,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 22,
                    Content = "계획부터 세워 보세요",
                    IsCorrect = false,
                    ListeningQuestionId = 6,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 23,
                    Content = "외국어 공부를 좀 할까 해요",
                    IsCorrect = true,
                    ListeningQuestionId = 6,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 24,
                    Content = "학기가 시작되면 많이 바빠요",
                    IsCorrect = false,
                    ListeningQuestionId = 6,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 25,
                    Content = "응.시골에서 산 적이 있어",
                    IsCorrect = false,
                    ListeningQuestionId = 7,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 26,
                    Content = "아니.너무 지루해서 졸았어",
                    IsCorrect = false,
                    ListeningQuestionId = 7,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 27,
                    Content = "아니.드라마 볼 시간이 없었어",
                    IsCorrect = false,
                    ListeningQuestionId = 7,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 28,
                    Content = "응.두 사람 보면서 한참 웃었어.",
                    IsCorrect = true,
                    ListeningQuestionId = 7,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 29,
                    Content = "만족도가 높은 편입니다",
                    IsCorrect = false,
                    ListeningQuestionId = 8,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 30,
                    Content = "조사 결과가 나왔습니다",
                    IsCorrect = false,
                    ListeningQuestionId = 8,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 31,
                    Content = "프로그램이 적은 것 같습니다",
                    IsCorrect = false,
                    ListeningQuestionId = 8,
                    CreatedDate = DateTime.Now
                },
                new ListeningAnswer
                {
                    Id = 32,
                    Content = "질문을 다시 정리해 보겠습니다",
                    IsCorrect = true,
                    ListeningQuestionId = 8,
                    CreatedDate = DateTime.Now
                }
                );
                
                    
        }
    }
}
