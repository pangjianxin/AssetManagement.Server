using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class OrganizationRoleDbConfig : IEntityTypeConfiguration<OrganizationRole>
    {
        public void Configure(EntityTypeBuilder<OrganizationRole> builder)
        {
            builder.HasKey(it => it.Id);
            builder.Property(it => it.Description).IsRequired().HasMaxLength(50);
        }
    }
}