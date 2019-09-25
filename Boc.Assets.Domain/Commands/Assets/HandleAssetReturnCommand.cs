using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetReturnCommand : AssetCommand
    {

        public HandleAssetReturnCommand(Guid eventId)
        {
            EventId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new HandleAssetReturnCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}