using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetExchangeCommand : ApplyCommand
    {

        public HandleAssetExchangeCommand(Guid eventId)
        {
            ApplyId = eventId;
        }
        public override bool IsValid()
        {
            ValidationResult = new HandleAssetExchangeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}