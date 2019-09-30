using Boc.Assets.Domain.Models.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class CategoryManageRegisterDbConfig : IEntityTypeConfiguration<CategoryManageRegister>
    {
        public void Configure(EntityTypeBuilder<CategoryManageRegister> builder)
        {
            //主键
            builder.HasKey(it => it.Id);
            //配置二级行为必须字段。因为各个行处理自己的事物，所以必须有二级行标记
            builder.Property(it => it.Org2).HasMaxLength(20).IsRequired();
            //配置与资产分类表的关系
            builder.HasOne(it => it.AssetCategory).WithMany(it => it.CategoryManageRegisters)
                .HasForeignKey(it => it.AssetCategoryId);
            //配置与机构表的关系
            builder.HasOne(it => it.Manager).WithMany(it => it.CategoryManageRegisters)
                .HasForeignKey(it => it.ManagerId);
        }
    }
}