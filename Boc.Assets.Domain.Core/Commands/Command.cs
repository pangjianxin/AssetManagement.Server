using Boc.Assets.Domain.Core.Events;
using FluentValidation.Results;
using System;

namespace Boc.Assets.Domain.Core.Commands
{
    /// <inheritdoc />
    /// <summary>
    /// 默认情况下Command返回一个布尔值
    /// </summary>
    public abstract class Command : Command<bool> { }
    /// <summary>
    /// Command
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class Command<TResult> : Message<TResult>
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