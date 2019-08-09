using Boc.Assets.Application.ViewModels;
using Boc.Assets.Domain.Core.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Extensions
{
    public class ModelStateActionFilter : IAsyncActionFilter
    {
        private readonly IUser _user;

        public ModelStateActionFilter(IUser user)
        {
            _user = user;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var count = 1;
                var finalMessage = new StringBuilder();
                var errors = context.ModelState.Values.SelectMany(it => it.Errors);
                foreach (var error in errors)
                {
                    var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                    finalMessage.Append($"{count++}:{errorMsg}");
                }
                context.Result =
                    new BadRequestObjectResult(new ActionHandleResult(false, finalMessage.ToString(), null));
            }
            else
            {
                foreach (KeyValuePair<string, object> argument in context.ActionArguments)
                {
                    if (argument.Value is ViewModel model)
                    {
                        model.Principal = _user;
                    }
                }
                await next();
            }
        }
    }
}