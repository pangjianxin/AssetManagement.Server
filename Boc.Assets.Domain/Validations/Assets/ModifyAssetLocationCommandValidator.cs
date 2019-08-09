using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Validations.Assets
{
    public class ModifyAssetLocationCommandValidator:AssetCommandValidator<ModifyAssetLocationCommand>
    {
        
        public ModifyAssetLocationCommandValidator()
        {
            ValidateAssetId();
            ValidateAssetLocation();
            ValidatePrincipal();
        }
    }
}