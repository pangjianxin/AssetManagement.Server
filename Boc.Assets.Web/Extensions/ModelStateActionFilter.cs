using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Extensions
{
    public class ModelStateActionFilter : IAsyncActionFilter
    {
        //因为在前端（angular）已经有对viewmodel的控制，所以，在这里不对viewmodel有校验，提高了性能。
        //同时在viewmodel向command转化后会对领域模型进行校验（command的校验）
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //尝试在这里获取一下
            //var domainNotification = context.Controller.GetType().GetProperty("Notifications", BindingFlags.NonPublic);
            //if (domainNotification?.GetValue(context.Controller) is DomainNotificationHandler handler && handler.HasNotifications())
            //{
            //    foreach (var value in handler.GetNotifications())
            //    {

            //    }
            //    context.Result = new BadRequestObjectResult(new ActionHandleResult(false, ""));
            //}
            //正文
            //if (!context.ModelState.IsValid)
            //{
            //    var count = 1;
            //    var finalMessage = new StringBuilder();
            //    var errors = context.ModelState.Values.SelectMany(it => it.Errors);
            //    foreach (var error in errors)
            //    {
            //        var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            //        finalMessage.Append($"{count++}:{errorMsg}");
            //    }
            //    context.Result =
            //        new BadRequestObjectResult(new ActionHandleResult(false, finalMessage.ToString(), null));
            //}
            //else
            //{
            //    await next();
            //}
            await next();
        }
    }
}