using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RevokeAssetApplyCommand : ApplyCommand
    {
        public RevokeAssetApplyCommand(Guid eventId, string message)
        {
            ApplyId = eventId;
            Message = message;
        }
        public override bool IsValid()
        {
            ValidationResult = new RevokeAssetApplyCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}