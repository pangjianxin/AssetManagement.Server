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
    /// <summary>
    /// 资产申请控制器，主要功能包括资产的申请，资产申请事件的审核等功能
    /// </summary>
    [Route("api/assetApply")]
    public class AssetApplyCommandController : ApiController
    {
        private readonly IAssetApplyService _assetApplyService;
        private readonly IAssetService _assetService;

        public AssetApplyCommandController(INotificationHandler<DomainNotification> notifications,
            IAssetApplyService assetApplyService,
            IAssetService assetService,
            IUser user)
            : base(notifications, user)
        {
            _assetService = assetService;
            _assetApplyService = assetApplyService;
        }
        /// <summary>
        /// 当前机构发起资产申请的API
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Post([FromBody] ApplyAsset model)
        {
            await _assetApplyService.ApplyAssetAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 处理机构发起的资产申请API
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("handle")]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Put([FromBody]HandleAssetApply model)
        {
            await _assetApplyService.HandleAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 撤销机构资产申请事件
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("revoke")]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Put([FromBody]RevokeAssetApply model)
        {
            await _assetApplyService.RevokeAsync(model);
            return AppResponse(null, "事件已撤销");
        }

        /// <summary>
        /// 删除机构资产申请事件
        /// 当前机构权限
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Delete(RemoveAssetApply model)
        {
            await _assetApplyService.RemoveAsync(model);
            return AppResponse(null, "事件已删除");
        }
    }
}