using Boc.Assets.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.EventDbContextConfig
{
    public class NonAuditEventDbConfig : IEntityTypeConfiguration<NonAuditEvent>
    {
        public void Configure(EntityTypeBuilder<NonAuditEvent> builder)
        {
            builder.HasKey(it => it.Id);
            builder.Property(it => it.Org2).IsRequired().HasMaxLength(20);
            builder.Property(it => it.OrgIdentifier).IsRequired().HasMaxLength(20);
            builder.Property(it => it.OrgNam).IsRequired().HasMaxLength(50);
            builder.Property(it => it.Type).IsRequired();
        }
    }
}