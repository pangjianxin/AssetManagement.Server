using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;

namespace Boc.Assets.Infrastructure.Repository
{
    public class CategoryManageRegisterRepository : EfCoreRepositoryBase<CategoryManageRegister>, ICategoryManageRegisterRepository
    {
        public CategoryManageRegisterRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}