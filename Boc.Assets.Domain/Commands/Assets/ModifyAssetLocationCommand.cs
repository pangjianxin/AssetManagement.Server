using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Assets;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class ModifyAssetLocationCommand : AssetCommand
    {
        public ModifyAssetLocationCommand(IUser principal, Guid assetId, string assetInStoreLocation) : base(principal)
        {
            AssetId = assetId;
            AssetLocation = assetInStoreLocation;
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ModifyAssetLocationCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}