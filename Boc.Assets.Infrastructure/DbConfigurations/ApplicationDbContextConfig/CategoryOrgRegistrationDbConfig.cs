using Boc.Assets.Domain.Models.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class CategoryOrgRegistrationDbConfig : IEntityTypeConfiguration<CategoryOrgRegistration>
    {
        public void Configure(EntityTypeBuilder<CategoryOrgRegistration> builder)
        {
            builder.HasKey(it => it.Id);
            builder.Property(it => it.Org2).HasMaxLength(20);
            builder.HasOne(it => it.AssetCategory).WithMany(it => it.CategoryOrgRegistrations)
                .HasForeignKey(it => it.AssetCategoryId);
            builder.HasOne(it => it.Organization).WithMany(it => it.CategoryOrgRegistrations)
                .HasForeignKey(it => it.OrganizationId);
        }
    }
}