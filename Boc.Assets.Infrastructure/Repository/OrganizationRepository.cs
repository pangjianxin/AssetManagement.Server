using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository
{
    public class OrganizationRepository : EfCoreRepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext context) : base(context)
        {
        }
        #region read
        public async Task<Organization> GetByOrgIdentifierAsync(string orgIdentifier)
        {
            var org = await Context.Set<Organization>().SingleAsync(it => it.OrgIdentifier == orgIdentifier);
            return org;
        }

        public override IQueryable<Organization> GetAll(Expression<Func<Organization, bool>> predicate = null)
        {
            return predicate == null ? DbSet : DbSet.Where(predicate);
        }
        #endregion

    }
}