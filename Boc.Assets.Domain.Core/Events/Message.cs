using MediatR;
using System;

namespace Boc.Assets.Domain.Core.Events
{
    public abstract class Message : Message<bool> { }
    public abstract class Message<TResult> : IRequest<TResult>
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}