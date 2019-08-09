using Boc.Assets.Domain.Core.Events;
using System;

namespace Boc.Assets.Domain.Core.Notifications
{
    /// <summary>
    /// 目前主要用来装载Command没有通过验证的信息。
    /// Command.MessageType是key，FluentValidation中的错误信息是value。
    /// </summary>
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; }
        public string Key { get; }
        public string Value { get; }
        public int Version { get; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}