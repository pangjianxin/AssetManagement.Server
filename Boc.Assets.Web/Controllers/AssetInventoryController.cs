using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Assets;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Web.Controllers
{
    public class AssetInventoryController : ODataController
    {
        private readonly IAssetInventoryService _assetInventoryService;
        private readonly IOrganizationService _orgService;
        private readonly IAssetService _assetService;
        private readonly IUser _user;

        public AssetInventoryController(
            IUser user,
            IAssetInventoryService assetInventoryService,
            IOrganizationService orgService,
            IAssetService assetService)
        {
            _user = user;
            _assetInventoryService = assetInventoryService;
            _orgService = orgService;
            _assetService = assetService;
        }
        /// <summary>
        /// 二级资产盘点任务列表
        /// 普通权限
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetInventoryDto> GetManageInventories(int year)
        {
            Expression<Func<AssetInventory, bool>> predicate = it => it.PublisherId == _user.OrgId && it.CreateDateTime.Year == year;
            return _assetInventoryService.GetInventories(predicate);
        }
        /// <summary>
        /// 根据指定的盘点任务索引找到所有的参与机构
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetInventoryResgiterDto> GetManageInventoryRegisters(Guid inventoryId)
        {
            Expression<Func<AssetInventoryRegister, bool>> predicate = it => it.AssetInventoryId == inventoryId;
            return _assetInventoryService.GetInventoryRegisters(predicate);

        }
        /// <summary>
        /// 查询未盘点资产
        /// </summary>
        /// <param name="model"></param>
        /// <param name="assetStockTakingOrgId"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetDto> GetAssetsWithoutInventory(Guid assetInventoryRegisterId)
        {

            Expression<Func<Asset, bool>> predicate = it => it.OrganizationInUseId == _user.OrgId
                                                            && !it.AssetInventoryDetails.Any(that => that.AssetInventoryRegisterId == assetInventoryRegisterId && that.AssetId == it.Id);
            var queryable = _assetInventoryService.GetAssetsWithOutInventory(predicate);
            return queryable;
        }
        /// <summary>
        /// 查询某一次资产盘点任务的已盘点清单
        /// </summary>
        /// <param name="assetInventoryRegisterId"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetInventoryDetailDto> GetUserDetails(Guid assetInventoryRegisterId)
        {
            Expression<Func<AssetInventoryDetail, bool>> predicate = it =>
                it.AssetInventoryRegisterId == assetInventoryRegisterId;
            return _assetInventoryService.GetInventoryDetails(predicate);
        }
    }
}