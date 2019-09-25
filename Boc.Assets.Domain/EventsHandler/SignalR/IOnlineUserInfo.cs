using System.Collections.Generic;

namespace Boc.Assets.Domain.EventsHandler.SignalR
{
    /// <summary>
    /// 记录当前SignalR在线用户数
    /// </summary>
    public interface IOnlineUserInfo
    {
        bool IsOnline(string orgIdentifier);
        bool AddUpdate(string orgIdentifier, string orgName);
        void Remove(string orgIdentifier);
        IEnumerable<dynamic> GetAllOnlineOrganizations();

    }
}