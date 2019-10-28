using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.AssetCategory;
using Boc.Assets.Domain.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetCategoryService : IApplicationService
    {
        IQueryable<AssetCategoryDto> Get(Expression<Func<AssetCategory, bool>> predicate = null);
        Task ChangeMeteringUnit(ChangeMeteringUnit model);
    }
}