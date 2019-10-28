using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetInventoryRepository : EfCoreRepositoryBase<AssetInventory>, IAssetInventoryRepository
    {
        public AssetInventoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public override IQueryable<AssetInventory> GetAll(Expression<Func<AssetInventory, bool>> predicate = null)
        //{
        //    return predicate == null ? DbSet : DbSet.Where(predicate);
        //}
    }
}