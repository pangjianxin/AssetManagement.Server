using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.Maintainers;
using Boc.Assets.Domain.Models.Assets;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IMaintainerService : IApplicationService
    {
        Task AddMaintainerAsync(AddMaintainer model);
        IQueryable<MaintainerDto> Get(Expression<Func<Maintainer, bool>> predicate);
        Task DeleteAsync(DeleteMaintainer model);
    }
}