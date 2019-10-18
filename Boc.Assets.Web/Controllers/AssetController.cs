using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    public class AssetController : ODataController
    {
        private readonly IAssetService _assetService;
        private readonly IUser _user;

        public AssetController(
            IUser user,
            IAssetService assetService)
        {
            _assetService = assetService;
            _user = user;
        }
        #region 普通用户资源
        /// <summary>
        /// 当前机构资产分页数据
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetDto> GetRurrent()
        {
            Expression<Func<Asset, bool>> predicate = it => it.OrganizationInUseId == _user.OrgId;
            return _assetService.Get(predicate);
        }
        /// <summary>
        /// 当前机构按三级分类的汇总数据
        /// 当前机构权限
        /// </summary>
        /// <returns></returns>
        //[EnableQuery]
        //[Authorize(Policy = "user")]
        //public async Task<IActionResult> CountByThirdLevelCurrent()
        //{
        //    Expression<Func<Asset, bool>> currentAssetsPredicate = it => it.OrganizationInUseId == _user.OrgId;
        //    var categories = await _assetService.CategoriesByThirdLevelAsync(currentAssetsPredicate);
        //}
        /// <summary>
        /// 当前机构按条线的汇总数量
        /// </summary>
        /// <returns></returns>
        //[EnableQuery]
        //[Authorize(Policy = "user")]
        //public async Task<IActionResult> CountByManagerOrgCurrent()
        //{
        //    Expression<Func<Asset, bool>> currentAssetsPredicate = it => it.OrganizationInUseId == _user.OrgId;
        //    var categories = await _assetService.CategoriesByManagerOrg(currentAssetsPredicate);
        //}
        #endregion
        #region 管理员资源
        /// <summary>
        /// 资产分页数据
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetDto> GetManage()
        {
            return _assetService.Get(it => it.OrganizationInChargeId == _user.OrgId);
        }
        /// <summary>
        /// 获取某个机构下的所有资产，
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orgInUseId"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetDto> GetCurrent(Guid orgInUseId)
        {
            Expression<Func<Asset, bool>> predicate = it => it.OrganizationInChargeId == _user.OrgId
                                                                         && it.OrganizationInUseId == orgInUseId;
            return _assetService.Get(predicate);
        }
        /// <summary>
        /// 按三级分类的分页数据
        /// 二级权限,用于资产申请分配资产
        /// </summary>
        /// <param name="model"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetDto> GetManageByCategory(Guid categoryId)
        {

            Expression<Func<Asset, bool>> predicate = it => it.OrganizationInChargeId == _user.OrgId
                                                            && it.AssetCategoryId == categoryId
                                                            && it.AssetStatus == AssetStatus.在库;
            return _assetService.Get(predicate);
        }
        /// <summary>
        /// 按三级分类索引的汇总数据
        /// 二级权限
        /// 这个放到AssetCategoryController里面会更好，以后改
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> CategoriesByThirdLevelSecondary()
        {
            Expression<Func<Asset, bool>> predicate = it => it.OrganizationInChargeId == _user.OrgId;
            var result = await _assetService.CategoriesByThirdLevelAsync(predicate);
            return Ok(result);
        }
        /// <summary>
        /// 按资产状态汇总数据
        /// 二级权限
        /// 这个放到AssetCategoryController里面会更好，以后改
        /// </summary>
        /// <returns></returns>
        //[EnableQuery]
        //[Authorize(Policy = "manage")]
        //public async Task<IActionResult> CategoriesByStatusSecondary()
        //{
        //    var result = await _assetService.CategoriesByStatusAsync(it => it.OrganizationInChargeId == _user.OrgId);
        //    return AppResponse(result);
        //}
        #endregion
    }
}