using Boc.Assets.Application.ViewModels;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boc.Assets.Web.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected DomainNotificationHandler Notifications { get; }
        protected readonly IUser _user;
        protected ApiController(INotificationHandler<DomainNotification> notifications, IUser user)
        {
            Notifications = notifications as DomainNotificationHandler;
            _user = user;
        }
        protected bool IsValidOperation()
        {
            return !Notifications.HasNotifications();
        }
        protected IActionResult AppResponse(object data = null, string message = null)
        {
            if (IsValidOperation())
            {
                return Ok(new ActionHandleResult(true, message, data));
            }
            var messages = Notifications.GetNotifications().Select(it => KeyValuePair.Create(it.Key, it.Value));
            var finalMessage = new StringBuilder();
            foreach (var item in messages)
            {
                finalMessage.Append($"{item.Key}:{item.Value}.");
            }
            return BadRequest(new ActionHandleResult(false, finalMessage.ToString(), data));
        }
        //protected void XPaginationHeader<T>(PaginatedList<T> pagination) where T : class
        //{
        //    var paginationHeader = new
        //    {
        //        pagination.PageSize,
        //        pagination.PageIndex,
        //        pagination.TotalItemsCount,
        //        pagination.PageCount,
        //    };
        //    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationHeader));
        //}
    }
}