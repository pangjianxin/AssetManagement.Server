using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RemoveAssetExchangeCommand : ApplyCommand
    {
        public RemoveAssetExchangeCommand(Guid eventId)
        {
            ApplyId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAssetExchangeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}