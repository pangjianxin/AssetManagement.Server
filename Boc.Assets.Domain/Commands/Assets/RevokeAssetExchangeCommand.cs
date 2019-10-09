using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RevokeAssetExchangeCommand : ApplyCommand
    {
        public RevokeAssetExchangeCommand(Guid eventId, string message)
        {
            ApplyId = eventId;
            Message = message;
        }
        public override bool IsValid()
        {
            ValidationResult = new RevokeAssetExchangeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}