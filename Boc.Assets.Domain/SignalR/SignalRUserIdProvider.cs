using Microsoft.AspNetCore.SignalR;

namespace Boc.Assets.Domain.SignalR
{
    public class SignalRUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            //tokenvalidationparameter中配置的RoleClaimType和NameClaimType在这里不起作用，要用原始的claim
            var orgIdentifier = connection.User.FindFirst("orgIdentifier")?.Value;
            if (string.IsNullOrEmpty(orgIdentifier))
            {
                return null;
            }
            return orgIdentifier;
        }
    }
}