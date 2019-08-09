using Boc.Assets.Domain.Models.Assets.Audit;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetExchangeRepository : EfCoreRepositoryBase<AssetExchange>, IAssetExchangeRepository
    {
        public AssetExchangeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}