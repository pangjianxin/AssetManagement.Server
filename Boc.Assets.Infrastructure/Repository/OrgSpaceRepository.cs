using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository
{
    public class OrgSpaceRepository : EfCoreRepositoryBase<OrganizationSpace>, IOrgSpaceRepository
    {
        public OrgSpaceRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<OrganizationSpace>> GetSpaces(Expression<Func<OrganizationSpace, bool>> predicate = null)
        {
            return await (predicate == null ? DbSet.ToListAsync() : DbSet.Where(predicate).ToListAsync());
        }
        public async Task<OrganizationSpace> CreateSpaceAsync(string spaceName, string spaceDescripption, Guid orgId, string orgIdentifier, string orgnam)
        {
            var space = new OrganizationSpace()
            {
                SpaceName = spaceName,
                SpaceDescription = spaceDescripption,
                OrgId = orgId,
                OrgIdentifier = orgIdentifier,
                OrgName = orgnam
            };
            await AddAsync(space);
            return space;
        }
        public async Task<OrganizationSpace> ModifySpaceAsync(Guid spaceId, string spaceName, string spaceDescription)
        {
            var space = await GetByIdAsync(spaceId);
            space.ModifySpaceInfo(spaceName, spaceDescription);
            Update(space);
            return space;
        }
    }
}