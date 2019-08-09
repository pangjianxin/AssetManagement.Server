using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository
{
    public class OrganizationRepository : EfCoreRepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext context) : base(context)
        {
        }
        #region update
        public async Task<Organization> ChangeOrgShortNameAsync(string orgIdentifier, string orgShortName)
        {
            var org = await GetByOrgIdentifierAsync(orgIdentifier);
            org.ChangeOrgShortName(orgShortName);
            Update(org);
            return org;
        }
        public async Task<Organization> ResetOrgPassword(string orgIdentifier)
        {
            var org = await GetByOrgIdentifierAsync(orgIdentifier);
            org.ResetPassword();
            Update(org);
            return org;
        }
        public async Task<Organization> ChangeOrgPassword(string orgIdentifier, string oldPassword, string newPassword)
        {
            var org = await GetByOrgIdentifierAsync(orgIdentifier);
            org.ChangeOrgPassword(newPassword);
            Update(org);
            return org;
        }
        #endregion
        #region read
        public async Task<Organization> GetByOrgIdentifierAsync(string orgIdentifier)
        {
            var org = await Context.Set<Organization>().SingleAsync(it => it.OrgIdentifier == orgIdentifier);
            return org;
        }
        #endregion

    }
}