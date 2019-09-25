using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.EventsHandler.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IOnlineUserInfo _onlineUserInfo;

        public ChatHub(IOnlineUserInfo onlineUserInfo)
        {
            _onlineUserInfo = onlineUserInfo;
        }
        public async Task Chat(string targetIdentifier, string message)
        {
            var principal = Context.UserIdentifier;
            var signalRMessage = new SignalRMessage(principal, targetIdentifier, message);
            await this.Clients.User(targetIdentifier).SendAsync("receiveMessage", signalRMessage);

        }
        public async Task<IEnumerable<dynamic>> AllOnLineOrgs()
        {
            return await Task.FromResult(_onlineUserInfo.GetAllOnlineOrganizations());
        }
        public override async Task OnConnectedAsync()
        {
            var orgIdentifier = Context.UserIdentifier;
            var orgName = Context.User.FindFirst(it => it.Type == "orgName").Value;
            _onlineUserInfo.AddUpdate(orgIdentifier, orgName);
            //通知客户端上线
            await Clients.All.SendAsync("orgLogin", new { orgIdentifier, orgName });
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var orgIdentifier = Context.UserIdentifier;
            _onlineUserInfo.Remove(orgIdentifier);
            //通知客户端下线
            await Clients.All.SendAsync("orgLogOut", orgIdentifier);
        }
    }
}