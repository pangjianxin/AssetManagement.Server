using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository
{
    public class PermissionRepository : EfCoreRepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task RemoveRangeByRoleId(Guid roleId)
        {
            var permissions = await GetAll(it => it.RoleId == roleId).ToListAsync();
            DbSet.RemoveRange(permissions);
        }
    }
}