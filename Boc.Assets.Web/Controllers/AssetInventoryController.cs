using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boc.Assets.Domain.Models.AssetInventories;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/assetStockTaking")]
    public class AssetInventoryController : ApiController
    {
        private readonly IAssetInventoryService _assetInventoryService;
        private readonly IOrganizationService _orgService;
        private readonly IAssetService _assetService;

        public AssetInventoryController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetInventoryService assetStockTakingService,
            IOrganizationService orgService,
            IAssetService assetService)
            : base(notifications, user)
        {
            _assetInventoryService = assetStockTakingService;
            _orgService = orgService;
            _assetService = assetService;
        }
        /// <summary>
        /// 创建资产盘点任务
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("secondary/create")]
        [Permission(Permissions.Controllers.AssetInventory, Permissions.Actions.AssetInventory_Create_Secondary)]
        public async Task<IActionResult> Create([FromBody]CreateAssetInventory model)
        {
            await _assetInventoryService.CreateAssetInventoryAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 二级资产盘点任务列表，获取二级行辖属所有分页数据
        /// 普通权限
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("secondary/list")]
        [Permission(Permissions.Controllers.AssetInventory, Permissions.Actions.AssetInventory_Read_Secondary)]
        public async Task<IActionResult> SecondaryList(int year)
        {
            Expression<Func<AssetInventory, bool>> predicate = it => it.PublisherOrg2 == _user.Org2 && it.CreateDateTime.Year == year;
            var listResult = await _assetInventoryService.AssetInventoriesAsync(year, predicate);
            return AppResponse(listResult, null);
        }

        [HttpGet("secondary/assetstocktakingorgs")]
        [Permission(Permissions.Controllers.AssetInventory, Permissions.Actions.AssetInventory_Read_Secondary)]
        public async Task<IActionResult> StockTakingOrgPagination(SieveModel model, Guid assetStockTakingId)
        {
            var paginatedList =
                await _assetInventoryService.AssetInventoriesPaginationAsync(model, assetStockTakingId);
            XPaginationHeader(paginatedList);
            return AppResponse(paginatedList, null);
        }

        [HttpGet("current/assetstocktakingorgs")]
        [Permission(Permissions.Controllers.AssetInventory, Permissions.Actions.AssetInventory_Read_Current)]
        public async Task<IActionResult> CurrentStockTakingOrgsList(int year)
        {
            Expression<Func<AssetInventoryRegister, bool>> predicate = it =>
                it.AssetInventory.CreateDateTime.Year == year
                && it.ParticipationId == _user.OrgId;
            var list = await _assetInventoryService.AssetInventoriesByYearAsync(year, predicate);
            return AppResponse(list, null);
        }
        /// <summary>
        /// 查询某个二级行下面是否有资产盘点任务
        /// 为减轻服务器压力
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("secondary/anystocktaking")]
        [Permission(Permissions.Controllers.AssetInventory, Permissions.Actions.AssetInventory_Read_Secondary)]
        public async Task<IActionResult> AnyStockTakingAsync(int year)
        {
            Expression<Func<AssetInventory, bool>> predicate = it =>
                it.PublisherOrg2 == _user.Org2 && it.CreateDateTime.Year == year;
            var any = await _assetInventoryService.AnyAssetInventoryAsync(year, predicate);
            return AppResponse(any);
        }
        /// <summary>
        /// 查询某个机构是否参与了资产盘点
        /// 为了减轻服务器压力
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("current/anystocktakingorgs")]
        [Permission(Permissions.Controllers.AssetInventory, Permissions.Actions.AssetInventory_Read_Current)]
        public async Task<IActionResult> AnyStocktakingOrgsAsync(int year)
        {
            Expression<Func<AssetInventoryRegister, bool>> predicate = it =>
                it.AssetInventory.CreateDateTime.Year == year
                && it.ParticipationId == _user.OrgId;
            var any = await _assetInventoryService.AnyAssetInventoryRegistersAsync(year, predicate);
            return AppResponse(any);
        }
        /// <summary>
        /// 查询未盘点资产
        /// </summary>
        /// <param name="model"></param>
        /// <param name="assetStockTakingOrgId"></param>
        /// <returns></returns>
        [HttpGet("current/assetswithoutstocktaking")]
        [Permission(Permissions.Controllers.AssetInventory, Permissions.Actions.AssetInventory_Read_Current)]
        public async Task<IActionResult> AssetWithOutInventory(SieveModel model, Guid assetStockTakingOrgId)
        {
            Expression<Func<Asset, bool>> predicate = it => it.OrgInUseName == _user.OrgIdentifier;
            PaginatedList<AssetDto> assetWithOutStockTaking = await _assetInventoryService.AssetsWithOutInventoriesAsync(model, assetStockTakingOrgId, predicate);
            XPaginationHeader(assetWithOutStockTaking);
            return AppResponse(assetWithOutStockTaking);
        }

        [HttpPost("current/createdetail")]
        [Permission(Permissions.Controllers.AssetInventory, Permissions.Actions.AssetInventory_Create_Current)]
        public async Task<IActionResult> CreateDetail([FromBody] CreateAssetInventoryDetail model)
        {
            await _assetInventoryService.CreatAssetInventoryDetailAsync(model);
            return AppResponse(null, "操作成功");
        }

        [HttpGet("current/assetstocktakingdetails")]
        [Permission(Permissions.Controllers.AssetInventory, Permissions.Actions.AssetInventory_Read_Current)]
        public async Task<IActionResult> StockTakingDetails(SieveModel model, Guid assetStockTakingOrgId)
        {
            var pagination =
                await _assetInventoryService.AssetInventoryDetailPaginationAsync(model, assetStockTakingOrgId);
            XPaginationHeader(pagination);
            return AppResponse(pagination, null);
        }
    }
}