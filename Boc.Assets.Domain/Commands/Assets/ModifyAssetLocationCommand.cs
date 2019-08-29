using Boc.Assets.Domain.Commands.Validations.Assets;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class ModifyAssetLocationCommand : AssetCommand
    {
        public ModifyAssetLocationCommand(IUser principal, Guid assetId, string assetInStoreLocation) : base(principal)
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