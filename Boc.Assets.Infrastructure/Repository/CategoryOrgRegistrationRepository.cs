using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class CategoryOrgRegistrationRepository : EfCoreRepositoryBase<CategoryOrgRegistration>, ICategoryOrgRegistrationRepository
    {
        public CategoryOrgRegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}