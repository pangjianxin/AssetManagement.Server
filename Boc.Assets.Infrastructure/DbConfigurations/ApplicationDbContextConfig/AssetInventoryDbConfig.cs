using Boc.Assets.Domain.Models.AssetInventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetInventoryDbConfig : IEntityTypeConfiguration<AssetInventory>
    {
        public void Configure(EntityTypeBuilder<AssetInventory> builder)
        {
            builder.HasKey(it => it.Id);
            builder.Property(it => it.PublisherName).IsRequired().HasMaxLength(50);
            builder.Property(it => it.PublisherIdentifier).IsRequired().HasMaxLength(20);
            builder.Property(it => it.PublisherOrg2).IsRequired().HasMaxLength(20);
            builder.Property(it => it.TaskName).IsRequired().HasMaxLength(50);
            builder.Property(it => it.TaskComment).IsRequired().HasMaxLength(50);
        }
    }
}