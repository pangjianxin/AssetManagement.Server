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

    }
}