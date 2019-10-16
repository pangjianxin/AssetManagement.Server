using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.AssetCategory;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/assetCategory")]
    public class AssetCategoryController : ApiController
    {
        private readonly IAssetCategoryService _assetCategoryService;

        public AssetCategoryController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetCategoryService assetCategoryService) : base(notifications, user)
        {
            _assetCategoryService = assetCategoryService;
        }
        /// <summary>
        /// 资产分类分页
        /// 二级权限
        /// 后期考虑对该api进行合并
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("secondary/pagination")]
        [Permission(Permissions.Controllers.AssetCategory, Permissions.Actions.AssetCategory_Read_Secondary)]
        public async Task<IActionResult> SecondaryPagination(SieveModel model)
        {
            var result = await _assetCategoryService.PaginationAsync(model, it => it.CategoryManageRegisters.Select(that => that.ManagerId).Contains(_user.OrgId));
            XPaginationHeader(result);
            return AppResponse(result);
        }
        /// <summary>
        /// 资产分类分页数据
        /// 当前用户权限
        /// 后期考虑对该api进行合并
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("current/pagination")]
        [Permission(Permissions.Controllers.AssetCategory, Permissions.Actions.AssetCategory_Read_Current)]
        public async Task<IActionResult> CurrentPaginationAsync(SieveModel model)
        {
            var result = await _assetCategoryService.PaginationAsync(model);
            XPaginationHeader(result);
            return AppResponse(result);
        }
        /// <summary>
        /// 资产分类计量单位
        /// 当前机构权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("current/units")]
        [Permission(Permissions.Controllers.AssetCategory, Permissions.Actions.AssetCategory_Read_Current)]
        public IActionResult GetMeteringUnits()
        {
            var dic = _assetCategoryService.GetMeteringUnits();
            return AppResponse(dic);
        }
        /// <summary>
        /// 修改资产分类计量单位
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("secondary/changeMeteringUnit")]
        [Permission(Permissions.Controllers.AssetCategory, Permissions.Actions.AssetCategory_Modify_Secondary)]
        public async Task<IActionResult> ChangeMeteringUnit([FromBody]ChangeMeteringUnit model)
        {
            await _assetCategoryService.ChangeMeteringUnit(model);
            return AppResponse(null, "修改成功");
        }

    }
}