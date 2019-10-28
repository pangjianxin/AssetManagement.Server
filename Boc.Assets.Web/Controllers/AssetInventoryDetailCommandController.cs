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
    [Route("api/assetInventoryDetail")]
    public class AssetInventoryDetailCommandController : ApiController
    {
        private readonly IAssetInventoryDetailService _assetInventoryDetailService;
        public AssetInventoryDetailCommandController(INotificationHandler<DomainNotification> notifications,
            IAssetInventoryDetailService detailService,
            IUser user)
        : base(notifications, user)
        {
            _assetInventoryDetailService = detailService;
        }

        [HttpPost]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Post([FromBody]CreateAssetInventoryDetail model)
        {
            await _assetInventoryDetailService.CreateAsync(model);
            return AppResponse(null, "创建成功");
        }

    }
}