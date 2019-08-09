using System;
using System.Threading.Tasks;
using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Models.Organizations;

namespace Boc.Assets.Domain.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task RemoveRangeByRoleId(Guid roleId);
    }
}