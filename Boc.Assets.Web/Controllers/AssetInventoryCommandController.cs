using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/inventroy")]
    public class AssetInventoryCommandController : ApiController
    {
        private readonly IAssetInventoryService _assetInventoryService;

        public AssetInventoryCommandController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IAssetInventoryService assetInventoryService)
            : base(notifications, user)
        {
            _assetInventoryService = assetInventoryService;
        }
        /// <summary>
        /// 创建资产盘点任务
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("manage/create")]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Post([FromBody]CreateAssetInventory model)
        {
            await _assetInventoryService.CreateAssetInventoryAsync(model);
            return AppResponse(null, "操作成功");
        }
    }
}