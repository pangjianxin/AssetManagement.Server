using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class PermissionDbConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(it => it.Id);
            builder.HasOne(it => it.OrganizationRole).WithMany(it => it.Permissions).HasForeignKey(it => it.RoleId);
            builder.Property(it => it.ControllerName).HasMaxLength(20).IsRequired();
            builder.Ignore(it => it.PermissionName);
            builder.Property(it => it.ActionName).HasMaxLength(20).IsRequired();
        }
    }
}