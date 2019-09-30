using Boc.Assets.Domain.Models.Applies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetExchangeDbConfig : IEntityTypeConfiguration<AssetExchange>
    {
        public void Configure(EntityTypeBuilder<AssetExchange> builder)
        {
            builder.HasKey(it => it.Id);
            builder.Property(it => it.RequestOrgIdentifier).IsRequired().HasMaxLength(20);
            builder.Property(it => it.RequestOrgNam).IsRequired().HasMaxLength(50);
            //builder.Property(it => it.Org2).IsRequired().HasMaxLength(20);
            builder.Property(it => it.TargetOrgIdentifier).IsRequired().HasMaxLength(20);
            builder.Property(it => it.TargetOrgNam).IsRequired().HasMaxLength(50);
            builder.Property(it => it.ExchangeOrgIdentifier).IsRequired().HasMaxLength(20);
            builder.Property(it => it.ExchangeOrgNam).IsRequired().HasMaxLength(50);
        }
    }
}