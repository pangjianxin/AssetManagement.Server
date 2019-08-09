using Boc.Assets.Domain.Models.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class MaintainerDbConfig : IEntityTypeConfiguration<Maintainer>
    {
        public void Configure(EntityTypeBuilder<Maintainer> builder)
        {
            builder.HasKey(it => it.Id);
            //公司名称
            builder.Property(it => it.CompanyName).IsRequired().HasMaxLength(50);
            //服务人员名称
            builder.Property(it => it.MaintainerName).IsRequired().HasMaxLength(20);
            //手机号
            builder.Property(it => it.Telephone).IsRequired().HasMaxLength(20);
            //办公电话
            builder.Property(it => it.OfficePhone).HasMaxLength(20);
            //二级行
            builder.Property(it => it.Org2).IsRequired().HasMaxLength(20);
            builder.Property(it => it.OrganizationId).IsRequired();
            //资产外键
            //配置和资产分类表的依赖关系
            builder.HasOne(it => it.AssetCategory).WithMany(it => it.Maintainers)
                .HasForeignKey(it => it.AssetCategoryId);
        }
    }
}