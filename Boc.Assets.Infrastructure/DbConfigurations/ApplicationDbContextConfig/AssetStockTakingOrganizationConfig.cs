using Boc.Assets.Domain.Models.AssetStockTakings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetStockTakingOrganizationConfig : IEntityTypeConfiguration<AssetStockTakingOrganization>
    {
        public void Configure(EntityTypeBuilder<AssetStockTakingOrganization> builder)
        {
            builder.HasKey(it => it.Id);
            builder.HasIndex(it => new { it.AssetStockTakingId, it.OrganizationId }).IsUnique();
            //配置资产盘点任务机构登记表的机构外键依赖
            builder.HasOne(it => it.Organization).WithMany(it => it.AssetStockTakingOrganizations)
                .HasForeignKey(it => it.OrganizationId);
            //配置资产盘点任务机构登记表归属盘点任务的外键依赖
            builder.HasOne(it => it.AssetStockTaking).WithMany(it => it.AssetStockTakingOrganizations)
                .HasForeignKey(it => it.AssetStockTakingId);

        }
    }
}