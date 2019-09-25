using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RevokeAssetExchangeCommand : AssetCommand
    {
        public RevokeAssetExchangeCommand(Guid eventId, string message)
        {
            EventId = eventId;
            Message = message;
        }
        public override bool IsValid()
        {
            ValidationResult = new RevokeAssetExchangeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}