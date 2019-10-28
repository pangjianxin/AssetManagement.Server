using Boc.Assets.Application.Dto;
using Boc.Assets.Domain.Models.Assets.TableViews;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetSumarryService : IApplicationService
    {
        IQueryable<ChartData> GetSumarryByCategory(Expression<Func<AssetSumarryByCategory, bool>> predicate = null);
        IQueryable<ChartData> GetSumarryByCount(Expression<Func<AssetSumarryByCount, bool>> predicate = null);
    }
}