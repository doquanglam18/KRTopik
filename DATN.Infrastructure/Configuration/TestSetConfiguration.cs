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
    public class TestSetConfiguration : IEntityTypeConfiguration<TestSet>
    {
        public void Configure(EntityTypeBuilder<TestSet> builder)
        {
            builder.ToTable(nameof(TestSet));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TestName).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.IsDelele).IsRequired().HasDefaultValue(false);
         builder.HasOne(x => x.RankQuestion).WithMany(r => r.TestSets).HasForeignKey(x => x.RankQuestionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasData(
            new TestSet
                {
                    Id = 1,
                    TestName = "Đề 1 Đọc Topik II 1 - 4",
                    CreatedDate = new DateTime(2024, 4, 30),
                    UpdatedDate = new DateTime(2024, 4, 30),
                    CreatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    UpdatedBy = Guid.Parse("5c0c563b-80d4-4485-9854-f6af58422601"),
                    IsDelele = false,
                    RankQuestionId = 1
                }
            );

        }
    }
}
