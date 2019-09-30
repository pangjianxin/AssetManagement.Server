using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetInventoryDetailRepository : EfCoreRepositoryBase<AssetInventoryDetail>, IAssetInventoryDetailRepository
    {
        public AssetInventoryDetailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}