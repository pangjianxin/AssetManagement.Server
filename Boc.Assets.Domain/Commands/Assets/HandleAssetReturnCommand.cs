using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetReturnCommand : ApplyCommand
    {

        public HandleAssetReturnCommand(Guid eventId)
        {
            ApplyId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new HandleAssetReturnCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}