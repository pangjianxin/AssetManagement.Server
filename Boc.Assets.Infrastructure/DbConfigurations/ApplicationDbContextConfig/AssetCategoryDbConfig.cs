using Boc.Assets.Domain.Models.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetCategoryDbConfig : IEntityTypeConfiguration<AssetCategory>
    {
        public void Configure(EntityTypeBuilder<AssetCategory> builder)
        {
            builder.HasKey(it => it.Id);
            builder.Property(it => it.AssetFirstLevelCategory).HasMaxLength(50).IsRequired();
            builder.Property(it => it.AssetSecondLevelCategory).HasMaxLength(50).IsRequired();
            builder.Property(it => it.AssetThirdLevelCategory).HasMaxLength(50).IsRequired();
            //配置外键关系--分类归属管理条线
            builder.HasOne(it => it.ManagementLine).WithMany(it => it.AssetCategories)
                .HasForeignKey(it => it.ManagementLineId);
        }
    }
}