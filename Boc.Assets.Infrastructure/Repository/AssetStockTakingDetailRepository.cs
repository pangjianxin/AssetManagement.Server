using Boc.Assets.Domain.Models.AssetStockTakings;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetStockTakingDetailRepository : EfCoreRepositoryBase<AssetStockTakingDetail>, IAssetStockTakingDetailRepository
    {
        public AssetStockTakingDetailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}