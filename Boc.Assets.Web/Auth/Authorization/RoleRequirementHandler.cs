using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Auth.Authorization
{
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var user = context.User;
            var userIdentifer = user.FindFirst(it => it.Type == "orgIdentifier")?.Value;
            if (userIdentifer == "A4640")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            var userRole = user.FindFirst(it => it.Type == "orgRole")?.Value;
            if (int.TryParse(userRole, out var parseResult))
            {
                if (parseResult >= requirement.RequiredRole)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}