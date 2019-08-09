using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Maintainers;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Maintainers
{
    public class DeleteMaintainerCommand : MaintainerCommand
    {
        public DeleteMaintainerCommand(IUser principal, Guid maintainerId) : base(principal)
        {
            MaintainerId = maintainerId;
        }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new DeleteMaintainerCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}