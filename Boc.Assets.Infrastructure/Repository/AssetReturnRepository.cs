using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetReturnRepository : EfCoreRepositoryBase<AssetReturn>, IAssetReturnRepository
    {
        public AssetReturnRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}