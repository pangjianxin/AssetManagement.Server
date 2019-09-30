using Boc.Assets.Domain.Models.AssetInventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetInventoryDetailDbConfig : IEntityTypeConfiguration<AssetInventoryDetail>
    {
        public void Configure(EntityTypeBuilder<AssetInventoryDetail> builder)
        {
            builder.HasKey(it => it.Id);
            builder.Property(it => it.ResponsibilityIdentity).IsRequired().HasMaxLength(20);
            builder.Property(it => it.ResponsibilityName).IsRequired().HasMaxLength(50);
            builder.Property(it => it.ResponsibilityOrg2).IsRequired().HasMaxLength(20);
            builder.Property(it => it.AssetInventoryLocation).IsRequired().HasMaxLength(100);
            //资产盘点机构登记表外键依赖配置
            builder.HasOne(it => it.AssetInventoryRegister).WithMany(it => it.AssetInventoryDetails)
                .HasForeignKey(it => it.AssetInventoryRegisterId);
            //资产外键依赖
            builder.HasOne(it => it.Asset).WithMany(it => it.AssetInventoryDetails)
                .HasForeignKey(it => it.AssetId);
        }
    }
}