using MediatR;
using System;

namespace Boc.Assets.Domain.Core.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTime TimeStamp { get; set; }
        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}