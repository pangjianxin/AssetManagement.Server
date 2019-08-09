using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetDeployRepository : EfCoreRepositoryBase<AssetDeploy>, IAssetDeployRepository
    {
        public AssetDeployRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}