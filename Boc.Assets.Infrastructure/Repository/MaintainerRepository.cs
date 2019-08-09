using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Repository
{
    public class MaintainerRepository : EfCoreRepositoryBase<Maintainer>, IMaintainerRepository
    {
        public MaintainerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> AnyAsync(Expression<Func<Maintainer, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }

        /// <summary>
        /// 查找指定的手机号是否在数据库里面已存在相关的记录，如果存在，就警告
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public async Task<bool> AnyTargetMaintainer(string telephone)
        {
            var maintainers = GetAll(it => it.Telephone == telephone);
            return await maintainers.AnyAsync();
        }

    }
}