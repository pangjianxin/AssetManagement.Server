using Microsoft.AspNetCore.Authorization;

namespace Boc.Assets.Web.Auth.Authorization
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {

        public PermissionAuthorizationRequirement(string controller, string action)
        {
            Controller = controller;
            Action = action;
        }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}