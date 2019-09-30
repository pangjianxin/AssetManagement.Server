using Boc.Assets.Domain.Models.AssetInventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetInventoryRegisterDbConfig : IEntityTypeConfiguration<AssetInventoryRegister>
    {
        public void Configure(EntityTypeBuilder<AssetInventoryRegister> builder)
        {
            builder.HasKey(it => it.Id);
            //配置资产盘点任务机构登记表的机构外键依赖
            builder.HasOne(it => it.Participation).WithMany(it => it.AssetInventoryRegisters)
                .HasForeignKey(it => it.ParticipationId);
            //配置资产盘点任务机构登记表归属盘点任务的外键依赖
            builder.HasOne(it => it.AssetInventory).WithMany(it => it.AssetInventoryRegisters)
                .HasForeignKey(it => it.AssetInventoryId);
        }
    }
}