using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Models.Organizations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Repositories
{
    public interface IOrgSpaceRepository : IRepository<OrganizationSpace>
    {
        Task<IEnumerable<OrganizationSpace>> GetSpaces(Expression<Func<OrganizationSpace, bool>> predicate = null);

        Task<OrganizationSpace> CreateSpaceAsync(string spaceName, string spaceDescripption, Guid orgId,
            string orgIdentifier, string orgnam);

        Task<OrganizationSpace> ModifySpaceAsync(Guid spaceId, string spaceName, string spaceDescription);
    }
}