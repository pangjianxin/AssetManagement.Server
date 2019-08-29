using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RemoveAssetReturnCommand : AssetCommand
    {
        public RemoveAssetReturnCommand(Guid eventId)
        {
            EventId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAssetReturnCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}