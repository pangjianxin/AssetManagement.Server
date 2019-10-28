using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.AssetInventories;
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
        private readonly IAssetService _assetService;
        private readonly IUser _user;

        public AssetInventoryController(
            IUser user,
            IAssetInventoryService assetInventoryService,
            IAssetService assetService)
        {
            _user = user;
            _assetInventoryService = assetInventoryService;
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
        public IQueryable<AssetInventoryDto> GetManage([FromODataUri]int year)
        {
            Expression<Func<AssetInventory, bool>> predicate = it => it.PublisherId == _user.OrgId && it.CreateDateTime.Year == year;
            return _assetInventoryService.GetInventories(predicate);
        }
    }
}