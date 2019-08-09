using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/assets")]
    public class AssetController : ApiController
    {
        private readonly IAssetService _assetService;

        public AssetController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetService assetService) : base(notifications, user)
        {
            _assetService = assetService;
        }
        #region 普通用户资源
        /// <summary>
        /// 当前机构资产分页数据
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("current")]
        [Permission(Permissions.Controllers.Asset, Permissions.Actions.Asset_Read_Current)]
        public async Task<IActionResult> PaginationCurrent(SieveModel model)
        {
            var result = await _assetService.PaginationAsync(model, it => it.OrganizationId == _user.OrgId);
            XPaginationHeader(result);
            return AppResponse(result);
        }
        /// <summary>
        /// 当前机构按三级分类的汇总数据
        /// 当前机构权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("current/categories/thirdLevel")]
        [Permission(Permissions.Controllers.Asset, Permissions.Actions.Asset_Read_Current)]
        public async Task<IActionResult> CountByThirdLevelCurrent()
        {
            var categories = await _assetService.CategoriesByThirdLevelAsync(it => it.OrganizationId == _user.OrgId);
            return AppResponse(categories);
        }
        /// <summary>
        /// 当前机构按条线的汇总数量
        /// </summary>
        /// <returns></returns>
        [HttpGet("current/categories/managerOrg")]
        [Permission(Permissions.Controllers.Asset, Permissions.Actions.Asset_Read_Current)]
        public async Task<IActionResult> CountByManagerOrgCurrent()
        {
            var categories = await _assetService.CategoriesByManagementLineAsync(it => it.OrganizationId == _user.OrgId);
            return AppResponse(categories);
        }
        /// <summary>
        /// 修改资产位置
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("current/modifyLocation")]
        [Permission(Permissions.Controllers.Asset, Permissions.Actions.Asset_Modify_Current)]
        public async Task<IActionResult> ModifyLocation([FromBody]ModifyAssetLocation model)
        {
            await _assetService.ModifyAssetLocationAsync(model);
            return AppResponse(null, "操作成功");
        }

        #endregion
        #region 管理员资源
        /// <summary>
        /// 资产分页数据
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("secondary/pagination")]
        [Permission(Permissions.Controllers.Asset, Permissions.Actions.Asset_Read_Secondary)]
        public async Task<IActionResult> PaginationSecondary(SieveModel model)
        {
            var result = await _assetService.PaginationAsync(model, it => it.Organization.Org2 == _user.Org2
                                                                          && it.AssetCategory.ManagementLineId == _user.ManagementLineId);
            XPaginationHeader(result);
            return AppResponse(result);
        }
        /// <summary>
        /// 按三级分类的分页数据
        /// 二级权限,用于资产申请分配资产
        /// </summary>
        /// <param name="model"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("secondary/pagination/thirdLevel")]
        [Permission(Permissions.Controllers.Asset, Permissions.Actions.Asset_Read_Secondary)]
        public async Task<IActionResult> PaginationByThirdLevelSecondary(SieveModel model, Guid categoryId)
        {

            Expression<Func<Asset, bool>> predicate = it => it.Organization.Org2 == _user.Org2
                                                            && it.AssetCategory.ManagementLineId == _user.ManagementLineId
                                                            && it.AssetCategoryId == categoryId
                                                            && it.AssetStatus == AssetStatus.在库;
            var result = await _assetService.PaginationAsync(model, predicate);
            XPaginationHeader(result);
            return AppResponse(result);
        }
        /// <summary>
        /// 按三级分类索引的汇总数据
        /// 二级权限
        /// 这个放到AssetCategoryController里面会更好，以后改
        /// </summary>
        /// <returns></returns>
        [HttpGet("secondary/categories/thirdLevel")]
        [Permission(Permissions.Controllers.Asset, Permissions.Actions.Asset_Read_Secondary)]
        public async Task<IActionResult> CategoriesByThirdLevelSecondary()
        {
            var result = await _assetService.CategoriesByThirdLevelAsync(it => it.Organization.Org2 == _user.Org2
                                                                               && it.AssetCategory.ManagementLineId ==
                                                                               _user.ManagementLineId);
            return AppResponse(result);
        }
        /// <summary>
        /// 按资产状态汇总数据
        /// 二级权限
        /// 这个放到AssetCategoryController里面会更好，以后改
        /// </summary>
        /// <returns></returns>
        [HttpGet("secondary/categories/status")]
        [Permission(Permissions.Controllers.Asset, Permissions.Actions.Asset_Read_Secondary)]
        public async Task<IActionResult> CategoriesByStatusSecondary()
        {
            var result = await _assetService.CategoriesByStatusAsync(it => it.Organization.Org2 == _user.Org2
                                                                           && it.AssetCategory.ManagementLineId ==
                                                                           _user.ManagementLineId);
            return AppResponse(result);
        }
        /// <summary>
        /// 资产入库（无文件）
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("secondary/storage")]
        [Permission(Permissions.Controllers.Asset, Permissions.Actions.Asset_Create_Current)]
        public async Task<IActionResult> StorageWithOutFile([FromBody]StoreAsset model)
        {
            await _assetService.StorageWithOutFile(model);
            return AppResponse(null, "入库成功");
        }
        #endregion
    }
}