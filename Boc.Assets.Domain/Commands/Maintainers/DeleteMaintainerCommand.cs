using Boc.Assets.Domain.Commands.Validations.Maintainers;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.Maintainers
{
    public class DeleteMaintainerCommand : MaintainerCommand
    {
        public DeleteMaintainerCommand(IUser principal, Guid maintainerId) : base(principal)
        {
            MaintainerId = maintainerId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteMaintainerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}