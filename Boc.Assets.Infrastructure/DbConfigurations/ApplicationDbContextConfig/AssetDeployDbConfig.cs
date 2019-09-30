using Boc.Assets.Domain.Models.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetDeployDbConfig : IEntityTypeConfiguration<AssetDeploy>
    {
        public void Configure(EntityTypeBuilder<AssetDeploy> builder)
        {   //主键
            builder.HasKey(it => it.Id);
            //资产名称
            builder.Property(it => it.AssetName).HasMaxLength(100).IsRequired();
            //资产索引
            builder.Property(it => it.AssetId).IsRequired();
            //资产标签号
            builder.Property(it => it.AssetTagNumber).HasMaxLength(100);
            //二级行标志
            //builder.Property(it => it.Org2).IsRequired().HasMaxLength(20);
            //资产编号
            builder.Property(it => it.AssetNo).HasMaxLength(50);
            //调出机构
            builder.OwnsOne(it => it.ExportOrgInfo, value =>
            {
                value.Property(it => it.OrgId).IsRequired();
                value.Property(it => it.OrgIdentifier).IsRequired().HasMaxLength(20);
                value.Property(it => it.OrgNam).IsRequired().HasMaxLength(50);

            });
            //调入机构
            builder.OwnsOne(it => it.ImportOrgInfo, value =>
            {
                value.Property(it => it.OrgId).IsRequired();
                value.Property(it => it.OrgIdentifier).IsRequired().HasMaxLength(20);
                value.Property(it => it.OrgNam).IsRequired().HasMaxLength(50);

            });
            //审批机构
            builder.OwnsOne(it => it.AuthorizeOrgInfo, value =>
            {
                value.Property(it => it.OrgId).IsRequired();
                value.Property(it => it.OrgIdentifier).IsRequired().HasMaxLength(20);
                value.Property(it => it.OrgNam).IsRequired().HasMaxLength(50);

            });

        }
    }
}