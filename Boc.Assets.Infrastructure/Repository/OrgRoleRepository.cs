using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class OrgRoleRepository : EfCoreRepositoryBase<OrganizationRole>, IOrgRoleRepository
    {
        public OrgRoleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}