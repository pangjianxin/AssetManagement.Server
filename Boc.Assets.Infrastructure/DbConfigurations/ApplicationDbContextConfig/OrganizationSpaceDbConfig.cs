using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class OrganizationSpaceDbConfig : IEntityTypeConfiguration<OrganizationSpace>
    {
        public void Configure(EntityTypeBuilder<OrganizationSpace> builder)
        {
            builder.HasKey(it => it.Id);
            builder.Property(it => it.SpaceName).HasMaxLength(100).IsRequired();
            builder.Property(it => it.SpaceDescription).HasMaxLength(100).IsRequired();
            builder.Property(it => it.OrgName).HasMaxLength(100).IsRequired();
            //配置外键关系
            builder.HasOne(it => it.Organization).WithMany(it => it.OrganizationSpaces).HasForeignKey(it => it.OrgId);
        }
    }
}