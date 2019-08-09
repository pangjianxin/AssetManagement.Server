using Boc.Assets.Domain.Models.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetDbConfig : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(it => it.Id);
            //资产名称
            builder.Property(it => it.AssetName).HasMaxLength(100).IsRequired();
            //资产序列号
            builder.Property(it => it.SerialNumber).HasMaxLength(100);
            //资产品牌
            builder.Property(it => it.Brand).HasMaxLength(50).IsRequired();
            //资产描述
            builder.Property(it => it.AssetDescription).HasMaxLength(100);
            //资产型号
            builder.Property(it => it.AssetType).HasMaxLength(100);
            //资产标签号
            builder.Property(it => it.AssetTagNumber).HasMaxLength(50);
            //资产编号
            builder.Property(it => it.AssetNo).HasMaxLength(100);
            //资产最后修改备注
            builder.Property(it => it.LastModifyComment).HasMaxLength(100);
            //配置外键关系--资产分类
            builder.HasOne(it => it.AssetCategory).WithMany(it => it.Assets)
                .HasForeignKey(it => it.AssetCategoryId);
            //配置外键关系--机构信息
            builder.HasOne(it => it.Organization).WithMany(it => it.Assets)
                .HasForeignKey(it => it.OrganizationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}