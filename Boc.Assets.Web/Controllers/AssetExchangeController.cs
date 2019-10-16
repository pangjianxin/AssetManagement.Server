using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Sieve.Models;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/assetExchange")]
    public class AssetExchangeController : ApiController
    {
        private readonly IAssetExchangeService _assetExchangeService;

        public AssetExchangeController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetExchangeService assetExchangeService)
            : base(notifications, user)
        {
            _assetExchangeService = assetExchangeService;
        }
        /// <summary>
        /// 资产调配分页数据
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("secondary/pagination")]
        [Permission(Permissions.Controllers.AssetExchange, Permissions.Actions.AssetExchange_Read_Secondary)]
        public async Task<IActionResult> PaginationSecondary(SieveModel model)
        {
            var pagination = await _assetExchangeService.PaginationAsync(model, it => it.TargetOrgId == _user.OrgId);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        /// <summary>
        /// 资产调配分页数据
        ///  当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("current/pagination")]
        [Permission(Permissions.Controllers.AssetExchange, Permissions.Actions.AssetExchange_Read_Current)]
        public async Task<IActionResult> PaginationCurrent(SieveModel model)
        {
            var pagination = await _assetExchangeService.PaginationAsync(model, it => it.RequestOrgId == _user.OrgId);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        /// <summary>
        /// 机构发起资产调配事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("exchange")]
        [Permission(Permissions.Controllers.AssetExchange, Permissions.Actions.AssetExchange_Create_Current)]
        public async Task<IActionResult> CreateAssetExchangeAsync([FromBody] ExchangeAsset model)
        {
            await _assetExchangeService.CreateAssetExchangeAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 处理机构资产调配事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("secondary/handle")]
        [Permission(Permissions.Controllers.AssetExchange, Permissions.Actions.AssetExchange_Modify_Secondary)]
        public async Task<IActionResult> HandleAssetExchangeAsync([FromBody] HandleAssetExchange model)
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
        [Permission(Permissions.Controllers.AssetExchange, Permissions.Actions.AssetExchange_Modify_Secondary)]
        public async Task<IActionResult> RevokeAssetExchangeAsync([FromBody]RevokeAssetExchange model)
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
        [HttpDelete("remove")]
        [Permission(Permissions.Controllers.AssetExchange, Permissions.Actions.AssetExchange_Delete_Current)]
        public async Task<IActionResult> Remove(RemoveAssetExchange model)
        {
            var @event = await _assetExchangeService.RemoveAssetExchangeAsync(model);
            return AppResponse(@event, "事件已删除");
        }
    }
}