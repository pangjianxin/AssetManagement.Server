using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Boc.Assets.Domain.EventsHandler.SignalR
{
    public class GroupChat : Hub
    {
        public async Task JoinGroup(string userId, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.User(userId).SendAsync("userJoined", $"用户{userId}加入群聊");
        }
        public async Task QuitGroup(string userId, string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.User(userId).SendAsync("userLogOut", $"用户{userId}退出群聊");
        }
        private void AbortConnect()
        {
            Context.Abort();
        }
    }
}