using Boc.Assets.Domain.Models.ManagementLines;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class ManagementLineDbConfig : IEntityTypeConfiguration<ManagementLine>
    {
        public void Configure(EntityTypeBuilder<ManagementLine> builder)
        {
            builder.HasKey(it => it.Id);
            builder.Property(it => it.ManagementLineName).IsRequired().HasMaxLength(50);
            builder.Property(it => it.ManagementLineDescription).IsRequired().HasMaxLength(50);
            builder.Property(it => it.CreateDateTime).IsRequired();
        }
    }
}