using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ViewModels.Maintainers;
using Boc.Assets.Domain.Models.Assets;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IMaintainerService : IApplicationService
    {
        Task AddMaintainerAsync(AddMaintainer model);
        Task<PaginatedList<MaintainerDto>> PaginationAsync(SieveModel model, Expression<Func<Maintainer, bool>> predicate = null);
        Task DeleteAsync(DeleteMaintainer model);
        Task<bool> AnyMaintainerAsync(Guid assetCategoryId, string org2);
        Task<List<MaintainerDto>> MaintainersByCategoryId(Guid categoryId, string org2);

    }
}