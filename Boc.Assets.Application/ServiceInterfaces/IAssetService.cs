using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetService : IApplicationService
    {
        /// <summary>
        /// 根据断言查找资产分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<PaginatedList<AssetDto>> PaginationAsync(SieveModel model, Expression<Func<Asset, bool>> predicate);
        /// <summary>
        /// 根据三级分类来汇总资产数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> CategoriesByThirdLevelAsync(Expression<Func<Asset, bool>> predicate);
        /// <summary>
        /// 根据资产状态汇总资产数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> CategoriesByStatusAsync(Expression<Func<Asset, bool>> predicate);
        /// <summary>
        /// 根据资产分类关联条线汇总资产数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> CategoriesByManagementLineAsync(Expression<Func<Asset, bool>> predicate);
        /// <summary>
        /// 修改资产存放位置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ModifyAssetLocationAsync(ModifyAssetLocation model);
        /// <summary>
        /// 无表批量资产入库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task StorageWithOutFile(StoreAsset model);
    }
}