using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class ModifyAssetLocationValidator:AssetValidator<ModifyAssetLocation>
    {
        public ModifyAssetLocationValidator()
        {
            ValidateAssetId();
            ValidateAssetLocation();
        }
    }
}