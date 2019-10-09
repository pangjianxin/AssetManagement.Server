using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RemoveAssetApplyCommand : ApplyCommand
    {
        public RemoveAssetApplyCommand(Guid eventId)
        {
            ApplyId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAssetApplyCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}