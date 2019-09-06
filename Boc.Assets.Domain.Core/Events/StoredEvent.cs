using System;

namespace Boc.Assets.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        // Ef core constructor
        public StoredEvent()
        {

        }
        public StoredEvent(Event @event, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            Data = data;
            User = user;
        }
        public Guid Id { get; private set; }
        public string Data { get; private set; }

        public string User { get; private set; }
        public virtual string DateTimeFromNow
        {
            get
            {
                var span = DateTime.Now - TimeStamp;
                return $"{span.Days}天,{span.Hours}小时,{span.Minutes}分钟,{span.Seconds}秒";
            }
        }
    }
}