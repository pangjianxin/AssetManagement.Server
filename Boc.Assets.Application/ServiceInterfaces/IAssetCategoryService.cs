using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.AssetCategory;
using Boc.Assets.Domain.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetCategoryService : IApplicationService
    {
        Task<PaginatedList<AssetCategoryDto>> PaginationAsync(SieveModel model,
            Expression<Func<AssetCategory, bool>> predicate = null);
        IEnumerable<dynamic> GetMeteringUnits();
        Task ChangeMeteringUnit(ChangeMeteringUnit model);
    }
}