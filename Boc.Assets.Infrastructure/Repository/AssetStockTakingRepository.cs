using Boc.Assets.Domain.Models.AssetStockTakings;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetStockTakingRepository : EfCoreRepositoryBase<AssetStockTaking>, IAssetStockTakingRepository
    {
        public AssetStockTakingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IQueryable<AssetStockTaking> GetAll(Expression<Func<AssetStockTaking, bool>> predicate = null)
        {
            return predicate == null ? DbSet : DbSet.Where(predicate);
        }
    }
}