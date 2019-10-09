using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RemoveAssetReturnCommand : ApplyCommand
    {
        public RemoveAssetReturnCommand(Guid eventId)
        {
            ApplyId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAssetReturnCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}