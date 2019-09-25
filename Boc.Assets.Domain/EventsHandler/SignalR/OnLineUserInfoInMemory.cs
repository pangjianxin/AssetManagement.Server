using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Boc.Assets.Domain.EventsHandler.SignalR
{
    /// <summary>
    /// 记录当前SignalR在线用户数
    /// </summary>
    public class OnLineUserInfoInMemory : IOnlineUserInfo
    {

        private ConcurrentDictionary<string, string> OnlineUser { get; set; } = new ConcurrentDictionary<string, string>();

        public bool AddUpdate(string orgIdentifier, string orgName)
        {

            var userAlreadyExists = OnlineUser.ContainsKey(orgIdentifier);
            OnlineUser.AddOrUpdate(orgIdentifier, orgName, (key, value) => value);

            return userAlreadyExists;
        }
        public void Remove(string orgIdentifier)
        {
            OnlineUser.TryRemove(orgIdentifier, out var userInfo);
        }
        public IEnumerable<dynamic> GetAllOnlineOrganizations()
        {
            if (OnlineUser.Any())
            {
                return OnlineUser.Select(it => new { orgIdentifier = it.Key, orgName = it.Value });
            }
            return null;
        }
        public bool IsOnline(string orgIdentifier)
        {
            return OnlineUser.ContainsKey(orgIdentifier);
        }
    }
}