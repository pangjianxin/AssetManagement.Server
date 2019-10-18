using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/assets")]
    public class AssetCommandController : ApiController
    {
        private readonly IAssetService _assetService;

        public AssetCommandController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetService assetService) : base(notifications, user)
        {
            _assetService = assetService;
        }
        /// <summary>
        /// 修改资产位置
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("current/modifyLocation")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Put([FromBody]ModifyAssetLocation model)
        {
            await _assetService.ModifyAssetLocationAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 资产入库（无文件）
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("secondary/storage")]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> StorageWithOutFile([FromBody]StoreAsset model)
        {
            await _assetService.StorageWithOutFile(model);
            return AppResponse(null, "入库成功");
        }
    }
}