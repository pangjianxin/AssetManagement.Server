using Boc.Assets.Domain.Core.SharedKernel;
using FluentValidation.Results;
using MediatR;
using System;

namespace Boc.Assets.Domain.Core.Commands
{
    public abstract class Command : IRequest<bool>
    {
        public DateTime Timestamp { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }
        public abstract bool IsValid();
    }
}