using System.Threading.Tasks;

namespace Boc.Assets.Domain.SignalR
{
    /// <summary>
    /// signalR的事件通知
    /// </summary>
    public interface IEventNotification
    {
        Task Notify(string principal, string time, string message);
    }
}