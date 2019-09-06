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
    }
}