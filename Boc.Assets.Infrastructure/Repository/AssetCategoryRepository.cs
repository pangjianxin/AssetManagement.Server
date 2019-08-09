using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetCategoryRepository : EfCoreRepositoryBase<AssetCategory>, IAssetCategoryRepository
    {
        public AssetCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task ChangeMeteringUnitAsync(Guid assetCategoryId, AssetMeteringUnit meteringUnit)
        {
            var category = await GetByIdAsync(assetCategoryId);
            category.AssetMeteringUnit = meteringUnit;
        }
    }
}