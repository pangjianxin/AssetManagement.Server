using Boc.Assets.Domain.Core.Events;
using FluentValidation.Results;
using System;

namespace Boc.Assets.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        protected Command()
        {
            Timestamp = DateTime.Now;
        }
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }
        public abstract bool IsValid();
    }
}