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
    [Route("api/assetExchange")]
    public class AssetExchangeCommandController : ApiController
    {
        private readonly IAssetExchangeService _assetExchangeService;

        public AssetExchangeCommandController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetExchangeService assetExchangeService)
            : base(notifications, user)
        {
            _assetExchangeService = assetExchangeService;
        }


        /// <summary>
        /// 机构发起资产调配事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Post([FromBody] ExchangeAsset model)
        {
            await _assetExchangeService.CreateAssetExchangeAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 处理机构资产调配事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("handle")]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Put([FromBody] HandleAssetExchange model)
        {
            await _assetExchangeService.HandleAssetExchangeAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 撤销机构调配事件
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("revoke")]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Put([FromBody]RevokeAssetExchange model)
        {
            await _assetExchangeService.RevokeAssetExchangeAsync(model);
            return AppResponse(null, "事件已撤销");
        }

        /// <summary>
        /// 删除机构调配事件
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Delete(RemoveAssetExchange model)
        {
            var @event = await _assetExchangeService.RemoveAssetExchangeAsync(model);
            return AppResponse(@event, "事件已删除");
        }
    }
}