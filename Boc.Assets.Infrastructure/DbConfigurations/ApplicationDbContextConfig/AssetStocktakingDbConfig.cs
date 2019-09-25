using Boc.Assets.Domain.Models.AssetStockTakings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class AssetStocktakingDbConfig : IEntityTypeConfiguration<AssetStockTaking>
    {
        public void Configure(EntityTypeBuilder<AssetStockTaking> builder)
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