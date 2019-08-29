using Boc.Assets.Domain.Commands.Validations.Maintainers;
using System;

namespace Boc.Assets.Domain.Commands.Maintainers
{
    public class DeleteMaintainerCommand : MaintainerCommand
    {
        public DeleteMaintainerCommand(Guid maintainerId)
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