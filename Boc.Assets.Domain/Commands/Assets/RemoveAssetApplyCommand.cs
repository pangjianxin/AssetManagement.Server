using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RemoveAssetApplyCommand : AssetCommand
    {
        public RemoveAssetApplyCommand(Guid eventId)
        {
            EventId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAssetApplyCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}