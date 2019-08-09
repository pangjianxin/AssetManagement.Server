using Boc.Assets.Domain.Core.SharedKernel;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Core.Commands
{
    public abstract class Command : IRequest<bool>
    {
        public IUser Principal { get; protected set; }
        public DateTime Timestamp { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }

        protected Command(IUser principal)
        {
            Principal = principal;
            Timestamp = DateTime.Now;
        }

        public abstract Task<bool> IsValid();
    }
}