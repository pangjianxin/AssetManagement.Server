using Boc.Assets.Domain.Models.AssetStockTakings;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetStockTakingOrganizationRepository : EfCoreRepositoryBase<AssetStockTakingOrganization>, IAssetStockTakingOrganizationRepository
    {
        public AssetStockTakingOrganizationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IQueryable<AssetStockTakingOrganization> GetAll(Expression<Func<AssetStockTakingOrganization, bool>> predicate = null)
        {
            return predicate == null ? DbSet : DbSet.Where(predicate);
        }

    }
}