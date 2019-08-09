using Boc.Assets.Domain.Models.ManagementLines;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class ManagementLineRepository : EfCoreRepositoryBase<ManagementLine>, IManagementLineRepository
    {
        public ManagementLineRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}