using Boc.Assets.Domain.Models.AssetStockTakings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetStockTakingDetailConfig : IEntityTypeConfiguration<AssetStockTakingDetail>
    {
        public void Configure(EntityTypeBuilder<AssetStockTakingDetail> builder)
        {
            builder.HasKey(it => it.Id);

            //资产盘点机构登记表外键依赖配置
            builder.HasOne(it => it.AssetStockTakingOrganization).WithMany(it => it.AssetStockTakingDetails)
                .HasForeignKey(it => it.AssetStockTakingOrganizationId);
            //资产外键依赖
            builder.HasOne(it => it.Asset).WithMany(it => it.AssetStockTakingDetails).HasForeignKey(it => it.AssetId);
            builder.Property(it => it.ResponsibilityIdentity).IsRequired().HasMaxLength(20);
            builder.Property(it => it.ResponsibilityName).IsRequired().HasMaxLength(50);
            builder.Property(it => it.ResponsibilityOrg2).IsRequired().HasMaxLength(20);
            builder.Property(it => it.AssetStockTakingLocation).IsRequired().HasMaxLength(100);
        }
    }
}