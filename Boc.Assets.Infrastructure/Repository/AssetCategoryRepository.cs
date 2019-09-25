using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetCategoryRepository : EfCoreRepositoryBase<AssetCategory>, IAssetCategoryRepository
    {
        public AssetCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}