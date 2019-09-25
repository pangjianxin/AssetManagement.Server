using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RevokeAssetReturnCommand : AssetCommand
    {
        public RevokeAssetReturnCommand(Guid eventId, string message)
        {
            EventId = eventId;
            Message = message;
        }
        public override bool IsValid()
        {
            ValidationResult = new RevokeAssetReturnCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}