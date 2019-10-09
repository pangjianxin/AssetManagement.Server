using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RevokeAssetReturnCommand : ApplyCommand
    {
        public RevokeAssetReturnCommand(Guid eventId, string message)
        {
            ApplyId = eventId;
            Message = message;
        }
        public override bool IsValid()
        {
            ValidationResult = new RevokeAssetReturnCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}