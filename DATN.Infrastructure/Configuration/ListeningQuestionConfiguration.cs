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
            builder.Property(x => x.ListeningSoundURL).IsRequired().HasMaxLength(500);
            builder.Property(x => x.IsPublic).IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.TestSet).WithMany(c => c.ListeningQuestions).HasForeignKey(x => x.TestSetId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.RankQuestion).WithMany(c => c.ListeningQuestions).HasForeignKey(x => x.RankQuestionId).OnDelete(DeleteBehavior.NoAction);


            builder.HasData(
                new ListeningQuestion
                {
                    Id = 1,
                    Question = "다음을 듣고 알맞은 그림을 고르십시오.",
                    ListeningSoundURL = "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038713/C1NgheDe61_pkin3e.mp3",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                    IsPublic = true,
                    RankQuestionId = 13,
                    ListeningScript = "여자: 무엇을 도와 드릴까요?<br>남자: 이 지갑,누가 잃어버린 것 같아요.이 앞에 있었어요.<br>여자: 네,이쪽으로 주세요.",
                    TestSetId = 2
                },
                new ListeningQuestion
                {
                    Id = 2,
                    Question = "다음을 듣고 알맞은 그림을 고르십시오.",
                    ListeningSoundURL = "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038714/C2NgheDe61_rtmjmt.mp3",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                    IsPublic = true,
                    RankQuestionId = 13,
                    ListeningScript = "남자:수미야,괜찮아?많이 아프겠다.<br>여자:응,다리가 아파서 못 일어나겠어.<br>남자:그래?내가 도와줄 테니까 천천히 일어나 봐",
                    TestSetId = 2
                },
                new ListeningQuestion
                {
                    Id = 3,
                    Question = "다음을 듣고 알맞은 그림을 고르십시오.",
                    ListeningSoundURL = "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038712/C3NgheDe61_n7u6p3.mp3",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                    IsPublic = true,
                    RankQuestionId = 13,
                    ListeningScript = "남자:직장인들은 점심시간을 어떻게 보낼까요?직장인의 점심시간은 한 시간이 70%였고,한 시간 삼십 분은 20%,한 시간 미만은 10%였습니다.식사 후 활동은 ‘동료와 차 마시기’가 가장 많았으며, ‘산책하기’, ‘낮잠 자기’가 뒤를 이었습니다.",
                    TestSetId = 2
                },
                new ListeningQuestion
                {
                    Id = 4,
                    Question = "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.",
                    ListeningSoundURL = "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038711/C4NgheDe61_tt1xxh.mp3",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                    IsPublic = true,
                    RankQuestionId = 14,
                    ListeningScript = "여자: 민수 씨,이번 주 모임 장소가 바뀌었대요.<br>남자: 그래요?어디로 바뀌었어요?<br>여자: ____________________________________________________",
                    TestSetId = 3
                },
                new ListeningQuestion
                {
                    Id = 5,
                    Question = "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.",
                    ListeningSoundURL = "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038713/C5NgheDe61_doyq1d.mp3",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                    IsPublic = true,
                    RankQuestionId = 14,
                    ListeningScript = "남자: 기차표 알아봤는데 금요일 오후 표는 없는 것 같아.<br>여자: 그럼 토요일 아침은 어때?<br>남자: ____________________________________________________",
                    TestSetId = 3
                },
                new ListeningQuestion
                {
                    Id = 6,
                    Question = "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.",
                    ListeningSoundURL = "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038712/C6NgheDe60_qjhl1h.mp3",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                    IsPublic = true,
                    RankQuestionId = 14,
                    ListeningScript = "여자:내일 발표만 끝나면 이제 이번 학기도 끝나네요.<br>남자:그러게요.수미 씨,방학 계획은 세웠어요?<br>여자: ____________________________________________________",
                    TestSetId = 3
                },
                new ListeningQuestion
                {
                    Id = 7,
                    Question = "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.",
                    ListeningSoundURL = "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038714/C7NgheDe60_r1ao0k.mp3",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                    IsPublic = true,
                    RankQuestionId = 14,
                    ListeningScript = "남자:이번에 새로 시작한 드라마 말이야.진짜 재미있더라.<br>여자: 아,그 시골에서 할머니랑 사는 아이 이야기?<br>남자: ____________________________________________________",
                    TestSetId = 3
                },
                new ListeningQuestion
                {
                    Id = 8,
                    Question = "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.",
                    ListeningSoundURL = "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038714/C8NgheDe60_o19qff.mp3",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = new Guid("5c0c563b-80d4-4485-9854-f6af58422601"),
                    IsPublic = true,
                    RankQuestionId = 14,
                    ListeningScript = "여자: 팀장님,프로그램 만족도 설문 조사를 만들어 봤는데요.확인해 주시겠어요?<br>남자:어디 봅시다.음,질문 수가 좀 많은 것 같네요.<br>여자: ____________________________________________________",
                    TestSetId = 3
                }

            );
        }
    }
}
