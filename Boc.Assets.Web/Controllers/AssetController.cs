using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.TableViews;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Web.Controllers
{
    public class AssetController : ODataController
    {
        private readonly IAssetService _assetService;
        private readonly IAssetSumarryService _assetsumarryservice;
        private readonly IUser _user;

        public AssetController(
            IUser user,
            IAssetService assetService,
            IAssetSumarryService assetSumarryService)
        {
            _assetService = assetService;
            _user = user;
            _assetsumarryservice = assetSumarryService;
        }
        /// <summary>
        /// 当前机构资产分页数据
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetDto> GetCurrent()
        {
            Expression<Func<Asset, bool>> predicate = it => it.OrganizationInUseId == _user.OrgId;
            return _assetService.Get(predicate);
        }
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
        /// 当前机构按三级分类的汇总数据
        /// 当前机构权限
        /// </summary>
        /// <returns>IEnumerable<ChartData></returns>
        [ODataRoute("GetCurrentSumarryByCategory")]
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<ChartData> GetCurrentSumarryByCategory()
        {
            Expression<Func<AssetSumarryByCategory, bool>> predicate = it => it.OrganizationInUseId == _user.OrgId;
            var sumarry = _assetsumarryservice.GetSumarryByCategory(predicate);
            return sumarry;
        }
        /// <summary>
        /// 按三级分类索引的汇总数据
        /// 二级权限
        /// 这个放到AssetCategoryController里面会更好，以后改
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        [ODataRoute("GetManageSumarryByCategory")]
        [Authorize(Policy = "manage")]
        public IQueryable<ChartData> GetManageSumarryByCategory()
        {
            Expression<Func<AssetSumarryByCategory, bool>> predicate = it => it.OrganizationInChargeId == _user.OrgId;
            var result = _assetsumarryservice.GetSumarryByCategory(predicate);
            return result;
        }

        [EnableQuery]
        [ODataRoute("GetManageSumarryByCount")]
        [Authorize(Policy = "manage")]
        public IQueryable<ChartData> GetManageSumarryByCount()
        {
            Expression<Func<AssetSumarryByCount, bool>> predicate = it => it.OrganizationInChargeId == _user.OrgId;
            var result = _assetsumarryservice.GetSumarryByCount(predicate);
            return result;
        }
        /// <summary>
        /// 查询未盘点资产
        /// </summary>
        /// <param name="model"></param>
        /// <param name="assetStockTakingOrgId"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetDto> GetAssetsWithoutInventory([FromODataUri]Guid assetInventoryRegisterId)
        {

            Expression<Func<Asset, bool>> predicate = it => it.OrganizationInUseId == _user.OrgId
                                                            && !it.AssetInventoryDetails.Any(that => that.AssetInventoryRegisterId == assetInventoryRegisterId);
            var queryable = _assetService.Get(predicate);
            return queryable;
        }
    }
}