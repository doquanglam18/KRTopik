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
    public class SystemLoggingConfiguration : IEntityTypeConfiguration<SystemLogging>
    {
        public void Configure(EntityTypeBuilder<SystemLogging> builder)
        {
            builder.ToTable(nameof(SystemLogging));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IPAddress).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ActionName).IsRequired();
            builder.Property(x => x.Details).IsRequired();
            builder.HasOne(x => x.User).WithMany(c => c.SystemLoggings).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

