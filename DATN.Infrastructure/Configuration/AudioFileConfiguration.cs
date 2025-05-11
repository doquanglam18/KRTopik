/*using DATN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Configuration
{
    public class AudioFileConfiguration : IEntityTypeConfiguration<AudioFile>
    {
        public void Configure(EntityTypeBuilder<AudioFile> builder)
        {
            builder.ToTable("AudioFile");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FileUrl).IsRequired();
            builder.Property(x => x.ContentType).IsRequired();
        }
    }
}
*/