using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig
{
    public class EmployeeDbConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(it => it.Id);
            builder.HasIndex(it => it.Identifier).IsUnique();
            builder.Property(it => it.Name).IsRequired().HasMaxLength(50);
            builder.Property(it => it.Identifier).IsRequired().HasMaxLength(50);
            builder.Property(it => it.Telephone).HasMaxLength(20);
            builder.Property(it => it.OfficePhone).HasMaxLength(20);
        }
    }
}