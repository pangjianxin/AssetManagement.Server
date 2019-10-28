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
    public class AssetInventoryRegisterController : ODataController
    {
        private readonly IAssetInventoryRegisterService _assetInventoryRegisterService;
        private readonly IUser _user;

        public AssetInventoryRegisterController(IAssetInventoryRegisterService assetInventoryRegisterService,
            IUser user)
        {
            _assetInventoryRegisterService = assetInventoryRegisterService;
            _user = user;
        }

        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetInventoryRegisterDto> GetCurrent([FromODataUri] int year)
        {
            Expression<Func<AssetInventoryRegister, bool>> predicate = it => it.ParticipationId == _user.OrgId
                                                                             && it.CreateDateTime.Year == year;
            return _assetInventoryRegisterService.Get(predicate);
        }

        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetInventoryRegisterDto> GetManage([FromODataUri] Guid inventoryId)
        {
            Expression<Func<AssetInventoryRegister, bool>> predicate = it => it.AssetInventoryId == inventoryId;
            return _assetInventoryRegisterService.Get(predicate);

        }
    }
}