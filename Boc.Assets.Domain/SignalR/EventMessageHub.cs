
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.SignalR
{
    public class EventMessageHub : Hub<IEventNotification>
    {

    }

    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(string userId, string message)
        {
            await this.Clients.User(userId).SendAsync("ReciveMessage", message);
        }
    }
}