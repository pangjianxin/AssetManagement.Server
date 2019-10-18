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
    [Route("api/assetReturn")]
    public class AssetReturnCommandController : ApiController
    {
        private readonly IAssetReturnService _assetReturnService;

        public AssetReturnCommandController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetReturnService assetReturnService)
            : base(notifications, user)
        {
            _assetReturnService = assetReturnService;
        }


        /// <summary>
        /// 机构资产申请发起
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Post([FromBody] ReturnAsset model)
        {
            await _assetReturnService.CreateAssetReturnAsync(model);
            return AppResponse(null, "操作成功");
        }

        /// <summary>
        /// 删除相应交回事件
        /// 当前权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Delete(RemoveAssetReturn model)
        {
            var apply = await _assetReturnService.RemoveAssetReturnAsync(model);
            return AppResponse(apply, "事件已删除");
        }
        /// <summary>
        /// 处理资产交回事件
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("manage/handle")]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Put([FromBody]HandleAssetReturn model)
        {
            await _assetReturnService.HandleAssetReturnAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 撤销机构资产交回事件
        /// 当前权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("manage/revoke")]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Put([FromBody]RevokeAssetReturn model)
        {
            await _assetReturnService.RevokeAssetReturnAsync(model);
            return AppResponse(null, "事件已撤销");
        }
    }
}