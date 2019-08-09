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
    [Route("api/assetReturn")]
    public class AssetReturnController : ApiController
    {
        private readonly IAssetReturnService _assetReturningEventService;

        public AssetReturnController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetReturnService assetReturningEventService)
            : base(notifications, user)
        {
            _assetReturningEventService = assetReturningEventService;
        }
        /// <summary>
        /// 前五条数据
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("secondary/pagination")]
        [Permission(Permissions.Controllers.AssetReturn, Permissions.Actions.AssetReturn_Read_Secondary)]
        public async Task<IActionResult> PaginationSecondary(SieveModel model)
        {
            var pagination = await _assetReturningEventService.PaginationAsync(model, it => it.TargetOrgId == _user.OrgId);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        /// <summary>
        /// 资产交回分页数据
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("current/pagination")]
        [Permission(Permissions.Controllers.AssetReturn, Permissions.Actions.AssetReturn_Read_Current)]
        public async Task<IActionResult> PaginationCurrent(SieveModel model)
        {
            var pagination = await _assetReturningEventService.PaginationAsync(model, it => it.RequestOrgId == _user.OrgId);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        /// <summary>
        /// 机构资产申请发起
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("return")]
        [Permission(Permissions.Controllers.AssetReturn, Permissions.Actions.AssetReturn_Create_Current)]
        public async Task<IActionResult> ReturnAsset([FromBody] ReturnAsset model)
        {
            await _assetReturningEventService.AssetReturnAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 删除相应交回事件
        /// 当前权限
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete("remove")]
        [Permission(Permissions.Controllers.AssetReturn, Permissions.Actions.AssetReturn_Delete_Current)]
        public async Task<IActionResult> Remove(Guid eventId)
        {
            var @event = await _assetReturningEventService.RemoveAsync(eventId);
            return AppResponse(@event, "事件已删除");
        }
        /// <summary>
        /// 处理资产交回事件
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("secondary/handle")]
        [Permission(Permissions.Controllers.AssetReturn, Permissions.Actions.AssetReturn_Modify_Secondary)]
        public async Task<IActionResult> Handle([FromBody]HandleAssetReturn model)
        {
            await _assetReturningEventService.HandleAsync(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 撤销机构资产交回事件
        /// 当前权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("revoke")]
        [Permission(Permissions.Controllers.AssetReturn, Permissions.Actions.AssetReturn_Modify_Secondary)]
        public async Task<IActionResult> Revoke([FromBody]Revoke model)
        {
            await _assetReturningEventService.RevokeAsync(model);
            return AppResponse(null, "事件已撤销");
        }
    }
}