using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class ModifyAssetLocationCommand : AssetCommand
    {
        public ModifyAssetLocationCommand(Guid assetId, string assetInStoreLocation)
        {
            AssetId = assetId;
            AssetLocation = assetInStoreLocation;
        }
        public override bool IsValid()
        {
            ValidationResult = new ModifyAssetLocationCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}