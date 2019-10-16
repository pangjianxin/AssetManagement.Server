using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Assets;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetInventoryService : IApplicationService
    {
        Task CreateAssetInventoryAsync(CreateAssetInventory model);
        Task<List<AssetInventoryDto>> AssetInventoriesAsync(int year, Expression<Func<AssetInventory, bool>> predicate);

        Task<PaginatedList<AssetInventoryResgiterDto>> AssetInventoriesPaginationAsync(SieveModel model, Guid stockTakingId);
        Task<List<AssetInventoryResgiterDto>> AssetInventoriesByYearAsync(int year, Expression<Func<AssetInventoryRegister, bool>> predicate);
        Task<bool> AnyAssetInventoryAsync(int year, Expression<Func<AssetInventory, bool>> predicate);
        /// <summary>
        /// 未经盘点的资产清单
        /// </summary>
        /// <returns></returns>
        Task<PaginatedList<AssetDto>> AssetsWithOutInventoriesAsync(SieveModel model, Guid assetStockTakingId, Expression<Func<Asset, bool>> predicate);
        Task<bool> AnyAssetInventoryRegistersAsync(int year, Expression<Func<AssetInventoryRegister, bool>> predicate);
        Task CreatAssetInventoryDetailAsync(CreateAssetInventoryDetail model);

        Task<PaginatedList<AssetInventoryDetailDto>> AssetInventoryDetailPaginationAsync(SieveModel model, Guid assetStockTakingOrgId);
    }
}