using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.AssetStockTakings;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.AssetStockTakings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetStockTakingService : IApplicationService
    {
        Task CreateAssetStockTaking(CreateAssetStockTaking model);
        Task<List<AssetStockTakingDto>> AssetStockTakingList(int year, Expression<Func<AssetStockTaking, bool>> predicate);

        Task<PaginatedList<AssetStockTakingOrgDto>> AssetStockTakingOrgPagination(SieveModel model, Guid stockTakingId);
        Task<List<AssetStockTakingOrgDto>> StockTakingOrgsInYear(int year, Expression<Func<AssetStockTakingOrganization, bool>> predicate);
        Task<bool> AnyStockTakingAsync(int year, Expression<Func<AssetStockTaking, bool>> predicate);
        /// <summary>
        /// 未经盘点的资产清单
        /// </summary>
        /// <returns></returns>
        Task<PaginatedList<AssetDto>> AssetsWithOutStockTaking(SieveModel model, Guid assetStockTakingId, Expression<Func<Asset, bool>> predicate);
        Task<bool> AnyStockTakingOrgsAsync(int year, Expression<Func<AssetStockTakingOrganization, bool>> predicate);
        Task CreateAssetStockTakingDetail(CreateAssetStockTakingDetail model);

        Task<PaginatedList<AssetStockTakingDetailDto>> StockTakingDetailPagination(SieveModel model, Guid assetStockTakingOrgId);
    }
}