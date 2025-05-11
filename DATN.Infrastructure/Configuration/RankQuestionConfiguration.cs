using DATN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DATN.Infrastructure.Configuration
{
    public class RankQuestionConfiguration : IEntityTypeConfiguration<RankQuestion>
    {
        public void Configure(EntityTypeBuilder<RankQuestion> builder)
        {
            builder.ToTable("RankQuestion");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.RankQuestionName).HasMaxLength(75).IsRequired();
            builder.Property(e => e.Note);
            builder.Property(e => e.CreatedDate).IsRequired();

            builder.HasMany(e => e.ReadingQuestions)
                   .WithOne(e => e.RankQuestion)
                   .HasForeignKey(e => e.RankQuestionId);

            builder.HasMany(e => e.ListeningQuestions)
                   .WithOne(e => e.RankQuestion)
                   .HasForeignKey(e => e.RankQuestionId);

            var now = DateTime.UtcNow;

            builder.HasData(
                new RankQuestion { Id = 1, RankQuestionName = "Đọc Topik II 1 - 4", Note = "Câu hỏi về ngữ pháp", CreatedDate = now },
                new RankQuestion { Id = 2, RankQuestionName = "Đọc Topik II 5 - 8", Note = "Xem tranh quảng cáo và chọn đáp án đúng", CreatedDate = now },
                new RankQuestion { Id = 3, RankQuestionName = "Đọc Topik II 9 - 12", Note = "Xem biểu đồ, đọc bài để chọn đáp án đúng", CreatedDate = now },
                new RankQuestion { Id = 4, RankQuestionName = "Đọc Topik II 13 - 15", Note = "Sắp xếp thứ tự câu cho đúng", CreatedDate = now },
                new RankQuestion { Id = 5, RankQuestionName = "Đọc Topik II 16 - 20", Note = "Chọn từ thích hợp để điền vào chỗ trống", CreatedDate = now },
                new RankQuestion { Id = 6, RankQuestionName = "Đọc Topik II 21 - 24", Note = "Đọc và chọn đáp án đúng", CreatedDate = now },
                new RankQuestion { Id = 7, RankQuestionName = "Đọc Topik II 25 - 27", Note = "Đọc đề mục tin tức và chọn đáp án giải thích đúng nhất", CreatedDate = now },
                new RankQuestion { Id = 8, RankQuestionName = "Đọc Topik II 28 - 31", Note = "Chọn câu phù hợp vào điền vào chỗ trống", CreatedDate = now },
                new RankQuestion { Id = 9, RankQuestionName = "Đọc Topik II 32 - 34", Note = "Điền nội dung đúng với đọan văn", CreatedDate = now },
                new RankQuestion { Id = 10, RankQuestionName = "Đọc Topik II 35 - 38", Note = "Chọn đáp án là nội dung chính của đoạn văn", CreatedDate = now },
                new RankQuestion { Id = 11, RankQuestionName = "Đọc Topik II 39 - 41", Note = "Chọn vị trí phù hợp để điền câu có sẵn vào", CreatedDate = now },
                new RankQuestion { Id = 12, RankQuestionName = "Đọc Topik II 42 - 50", Note = "Đọc đoạn văn dài và chọn đáp án phù hợp", CreatedDate = now },
                new RankQuestion { Id = 13, RankQuestionName = "Nghe Topik II 1 - 3", Note = "Nghe và chọn tranh đúng", CreatedDate = now },
                new RankQuestion { Id = 14, RankQuestionName = "Nghe Topik II 4 - 8", Note = "Chọn câu tiếp nối cho đoạn hội thoại", CreatedDate = now },
                new RankQuestion { Id = 15, RankQuestionName = "Nghe Topik II 9 - 12", Note = "Nghe và chọn hành động người nữ hoặc nam sẽ làm tiếp theo", CreatedDate = now },
                new RankQuestion { Id = 16, RankQuestionName = "Nghe Topik II 13 - 16", Note = "Nghe và chọn đáp án đúng", CreatedDate = now },
                new RankQuestion { Id = 17, RankQuestionName = "Nghe Topik II 17 - 20", Note = "Nghe và chọn suy nghĩ trọng tâm của nhân vật", CreatedDate = now },
                new RankQuestion { Id = 18, RankQuestionName = "Nghe Topik II 21 - 30", Note = "Nghe đoạn văn và chọn đáp án đúng (Gồm những đoạn văn dài và khó mức 1)", CreatedDate = now },
                new RankQuestion { Id = 19, RankQuestionName = "Nghe Topik II 31 - 50", Note = "Nghe đoạn văn và chọn đáp án đúng (Gồm những đoạn văn dài và khó mức 2)", CreatedDate = now },
                new RankQuestion { Id = 20, RankQuestionName = "Nghe Topik I", Note = "Nội dung nghe đề Topik I", CreatedDate = now },
                new RankQuestion { Id = 21, RankQuestionName = "Đọc Topik I", Note = "Nội dung đọc đề Topik I", CreatedDate = now }
            );
        }
    }
}
