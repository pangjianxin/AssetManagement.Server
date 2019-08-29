using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetExchangeCommand : AssetCommand
    {

        public HandleAssetExchangeCommand(Guid eventId)
        {
            EventId = eventId;
        }
        public override bool IsValid()
        {
            ValidationResult = new HandleAssetExchangeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}