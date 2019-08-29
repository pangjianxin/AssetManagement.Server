using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class RemoveAssetApplyValidator : AssetValidator<Remove>
    {
        public RemoveAssetApplyValidator()
        {
            ValidateEventId();
        }
    }
}