using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetApplyCommand : AssetCommand
    {

        public HandleAssetApplyCommand(Guid eventId, Guid assetId)
        {
            EventId = eventId;
            AssetId = assetId;
        }
        public override bool IsValid()
        {
            ValidationResult = new HandleAssetApplyCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}