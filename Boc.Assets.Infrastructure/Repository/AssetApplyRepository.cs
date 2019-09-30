using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetApplyRepository : EfCoreRepositoryBase<AssetApply>, IAssetApplyRepository
    {
        public AssetApplyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}