using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    /// <summary>
    /// 资产申请控制器，主要功能包括资产的申请，资产申请事件的审核等功能
    /// </summary>
    [Route("api/assetApply")]
    public class AssetApplyController : ApiController
    {
        private readonly IAssetApplyService _assetApplyService;
        private readonly IAssetService _assetService;

        public AssetApplyController(INotificationHandler<DomainNotification> notifications,
            IAssetApplyService assetApplyService,
            IAssetService assetService,
            IUser user)
            : base(notifications, user)
        {
            _assetService = assetService;
            _assetApplyService = assetApplyService;
        }
        /// <summary>
        /// 资产申请事件分页数据
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("secondary/pagination")]
        [Permission(Permissions.Controllers.AssetApply, Permissions.Actions.AssetApply_Read_Secondary)]
        public async Task<IActionResult> ReadSecondary(SieveModel model)
        {
            var pagination = await _assetApplyService.PaginationAsync(model, it => it.TargetOrgId == _user.OrgId);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        /// <summary>
        /// 当前机构资产申请事件分页数据
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("current/pagination")]
        [Permission(Permissions.Controllers.AssetApply, Permissions.Actions.AssetApply_Read_Current)]
        public async Task<IActionResult> ReadCurrent(SieveModel model)
        {
            var pagination = await _assetApplyService.PaginationAsync(model, it => it.RequestOrgId == _user.OrgId);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        /// <summary>
        /// 当前机构发起资产申请的API
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("apply")]
        [Permission(Permissions.Controllers.AssetApply, Permissions.Actions.AssetApply_Create_Current)]
        public async Task<IActionResult> ApplyAsset([FromBody] ApplyAsset model)
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
        [HttpPut("secondary/handle")]
        [Permission(Permissions.Controllers.AssetApply, Permissions.Actions.AssetApply_Modify_Secondary)]
        public async Task<IActionResult> Handle([FromBody]HandleAssetApply model)
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
        [HttpDelete("revoke")]
        [Permission(Permissions.Controllers.AssetApply, Permissions.Actions.AssetApply_Modify_Secondary)]
        public async Task<IActionResult> Revoke(RevokeAssetApply model)
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
        [HttpDelete("remove")]
        [Permission(Permissions.Controllers.AssetApply, Permissions.Actions.AssetApply_Delete_Current)]
        public async Task<IActionResult> Remove(RemoveAssetApply model)
        {
            await _assetApplyService.RemoveAsync(model);
            return AppResponse(null, "事件已删除");
        }
    }
}