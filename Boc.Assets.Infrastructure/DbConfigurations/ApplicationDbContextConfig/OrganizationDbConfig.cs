using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class OrganizationDbConfig : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(it => it.Id);
            //机构号
            builder.HasIndex(it => it.OrgIdentifier).IsUnique();
            //机构名称
            builder.Property(it => it.OrgNam).HasMaxLength(100).IsRequired();
            //机构短名称
            builder.Property(it => it.OrgShortNam).HasMaxLength(100).IsRequired();
            //上级机构
            builder.Property(it => it.UpOrg).HasMaxLength(10).IsRequired();
            //机构层级
            builder.Property(it => it.OrgLvl).HasMaxLength(10).IsRequired();
            //一级机构
            builder.Property(it => it.Org1).HasMaxLength(10).IsRequired();
            //一级机构名称
            builder.Property(it => it.OrgNam1).HasMaxLength(100).IsRequired();
            //二级机构
            builder.Property(it => it.Org2).HasMaxLength(10);
            //二级机构名称
            builder.Property(it => it.OrgNam2).HasMaxLength(100);
            //三级机构
            builder.Property(it => it.Org3).HasMaxLength(10);
            //三级机构名称
            builder.Property(it => it.OrgNam3).HasMaxLength(100);
            //配置外键关系--机构角色
            builder.HasOne(it => it.Role).WithMany(it => it.Organizations)
                .HasForeignKey(it => it.RoleId);
            //配置外键关系--机构归属条线
            builder.HasOne(it => it.ManagementLine).WithMany(it => it.Organizations)
                .HasForeignKey(it => it.ManagementLineId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}