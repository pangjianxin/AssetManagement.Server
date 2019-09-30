using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Infrastructure.Repository
{
    public class AssetInventoryRegisterRepository : EfCoreRepositoryBase<AssetInventoryRegister>, IAssetInventoryRegisterRepository
    {
        public AssetInventoryRegisterRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IQueryable<AssetInventoryRegister> GetAll(Expression<Func<AssetInventoryRegister, bool>> predicate = null)
        {
            return predicate == null ? DbSet : DbSet.Where(predicate);
        }

    }
}