using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetRepository : EfCoreRepositoryBase<Asset>, IAssetRepository
    {
        public AssetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Asset> ModifyAssetLocation(Guid assetId, string assetInstoredLocation)
        {
            var asset = await DbSet.FindAsync(assetId);
            asset.ModifyAssetLocation(assetInstoredLocation);
            Update(asset);
            return asset;
        }
    }
}