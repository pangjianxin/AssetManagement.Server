using Boc.Assets.Domain.Events;
using Boc.Assets.Infrastructure.DbConfigurations.EventDbContextConfig;
using Microsoft.EntityFrameworkCore;

namespace Boc.Assets.Infrastructure.DataBase
{
    public class EventStoreDbContext : DbContext
    {
        public DbSet<NonAuditEvent> NonAuditEvents { get; set; }
        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new NonAuditEventDbConfig());
        }
    }
}