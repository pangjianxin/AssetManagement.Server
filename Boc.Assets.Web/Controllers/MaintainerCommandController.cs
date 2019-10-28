using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Maintainers;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/maintainer")]
    public class MaintainerCommandController : ApiController
    {
        private readonly IMaintainerService _maintainerService;

        public MaintainerCommandController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMaintainerService maintainerService)
            : base(notifications, user)
        {
            _maintainerService = maintainerService;
        }

        [HttpPost]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Post([FromBody] AddMaintainer model)
        {
            await _maintainerService.AddMaintainerAsync(model);
            return AppResponse(null, "操作成功");
        }
        [HttpDelete]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> Delete(DeleteMaintainer model)
        {
            await _maintainerService.DeleteAsync(model);
            return AppResponse(null, "操作成功");
        }
    }
}