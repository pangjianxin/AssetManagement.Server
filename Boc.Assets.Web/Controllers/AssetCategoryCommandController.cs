using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.AssetCategory;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/assetCategory")]
    public class AssetCategoryCommandController : ApiController
    {
        private readonly IAssetCategoryService _assetCategoryService;

        public AssetCategoryCommandController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetCategoryService assetCategoryService) : base(notifications, user)
        {
            _assetCategoryService = assetCategoryService;
        }
        /// <summary>
        /// 修改资产分类计量单位
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Put([FromBody]ChangeMeteringUnit model)
        {
            await _assetCategoryService.ChangeMeteringUnit(model);
            return AppResponse(null, "修改成功");
        }

    }
}