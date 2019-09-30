using Boc.Assets.Domain.Models.Applies;
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