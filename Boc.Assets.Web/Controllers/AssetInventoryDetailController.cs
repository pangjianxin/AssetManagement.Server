using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Models.AssetInventories;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Web.Controllers
{
    public class AssetInventoryDetailController : ODataController
    {
        private readonly IAssetInventoryDetailService _assetInventoryDetailService;

        public AssetInventoryDetailController(IAssetInventoryDetailService assetInventoryDetailService)
        {
            _assetInventoryDetailService = assetInventoryDetailService;
        }

        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetInventoryDetailDto> GetCurrent([FromODataUri] Guid assetInventoryRegisterId)
        {
            Expression<Func<AssetInventoryDetail, bool>> predicate = it => it.AssetInventoryRegisterId == assetInventoryRegisterId;
            return _assetInventoryDetailService.Get(predicate);
        }
    }
}