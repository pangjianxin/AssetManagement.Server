using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RemoveAssetExchangeCommand : AssetCommand
    {
        public RemoveAssetExchangeCommand(Guid eventId)
        {
            EventId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAssetExchangeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}