using Microsoft.AspNetCore.Authorization;

namespace Boc.Assets.Web.Auth.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public int RequiredRole { get; }
        public RoleRequirement(int requiredRole)
        {
            RequiredRole = requiredRole;
        }
    }
}