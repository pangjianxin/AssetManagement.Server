using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.OrganizationSpace;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/orgspace")]
    public class OrgSpaceCommandController : ApiController
    {
        private readonly IOrgSpaceService _orgSpaceService;
        public OrgSpaceCommandController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IOrgSpaceService orgSpaceService) : base(notifications, user)
        {
            _orgSpaceService = orgSpaceService;
        }

        [HttpPost("create")]
        [Permission(Permissions.Controllers.OrganizationSpace, Permissions.Actions.OrgSpace_Create_Current)]
        public async Task<IActionResult> Post([FromBody] CreateSpace model)
        {
            await _orgSpaceService.CreateAsync(model);
            return AppResponse(null, "创建成功");
        }
        [HttpPut("update")]
        [Permission(Permissions.Controllers.OrganizationSpace, Permissions.Actions.OrgSpace_Modify_Current)]
        public async Task<IActionResult> Put([FromBody]ModifySpaceInfo model)
        {
            await _orgSpaceService.ModifyAsync(model);
            return AppResponse(null, "操作成功");
        }
    }
}