using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/nonauditevents")]
    public class NonAuditEventController : ApiController
    {
        private readonly INonAuditEventService _nonAuditEventService;

        public NonAuditEventController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            INonAuditEventService nonAuditEventService)
            : base(notifications, user)
        {
            _nonAuditEventService = nonAuditEventService;
        }

        [HttpGet("current/pagination")]
        [Permission(Permissions.Controllers.NonAuditEvent, Permissions.Actions.NonAuditEvent_Read_Current)]
        public async Task<IActionResult> CurrentPagination(SieveModel model)
        {
            var pagination = await _nonAuditEventService.PaginationAsync(model, it => it.OrgId == _user.OrgId);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        [HttpGet("secondaryadmin/pagination")]
        [Permission(Permissions.Controllers.NonAuditEvent, Permissions.Actions.NonAuditEvent_Read_Secondary)]
        public async Task<IActionResult> SecondaryAdminPagination(SieveModel model)
        {
            var pagination = await _nonAuditEventService.PaginationAsync(model, it => it.Org2 == _user.Org2);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }

        [HttpDelete("delete")]
        [Permission(Permissions.Controllers.NonAuditEvent, Permissions.Actions.NonAuditEvent_Delete_Current)]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            var @event = await _nonAuditEventService.RemoveAsync(eventId);
            return AppResponse(@event);
        }
    }
}