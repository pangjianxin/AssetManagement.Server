using System;
using Boc.Assets.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Auth.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IMemoryCache _cache;
        public PermissionAuthorizationHandler(IMemoryCache cache)
        {
            _cache = cache;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                if (context.User.FindFirst(it => it.Type == "orgIdentifier")?.Value == "A4640")
                {
                    context.Succeed(requirement);
                }
                else
                {
                    var role = context.User.FindFirst(it => it.Type == "roleId")?.Value;
                    if (!string.IsNullOrEmpty(role))
                    {
                        var permissionExist = _cache.CheckPermission(role, requirement.Controller, requirement.Action);
                        if (permissionExist)
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}