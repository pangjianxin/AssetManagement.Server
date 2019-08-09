﻿using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> ExchangeAssetAsync([FromBody] ExchangeAsset model)
        {
            await _assetExchangeService.AssetExchangeAsync(model);
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
            await _assetExchangeService.HandleAsync(model);
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
        public async Task<IActionResult> Revoke([FromBody]Revoke model)
        {
            await _assetExchangeService.RevokeAsync(model);
            return AppResponse(null, "事件已撤销");
        }
        /// <summary>
        /// 删除机构调配事件
        /// 当前机构权限
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete("remove")]
        [Permission(Permissions.Controllers.AssetExchange, Permissions.Actions.AssetExchange_Delete_Current)]
        public async Task<IActionResult> Remove(Guid eventId)
        {
            var @event = await _assetExchangeService.RemoveAsync(eventId);
            return AppResponse(@event, "事件已删除");
        }
    }
}