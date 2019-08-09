using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/managementline")]
    public class ManagementLineController : ApiController
    {
        private readonly IManagementLineService _managementLineService;

        public ManagementLineController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IManagementLineService managementLineService)
            : base(notifications, user)
        {
            _managementLineService = managementLineService;
        }

        [HttpGet("user/examinations")]
        [Permission(Permissions.Controllers.ManagementLine, Permissions.Actions.ManagementLine_Read)]
        public async Task<IActionResult> TargetExaminations(Guid managementLineId)
        {
            var orgDtos = await _managementLineService.GetTargetExaminations(managementLineId);
            return AppResponse(orgDtos);
        }
    }
}