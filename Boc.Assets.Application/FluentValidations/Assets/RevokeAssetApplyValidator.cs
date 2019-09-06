using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class RevokeAssetApplyValidator:AssetValidator<RevokeAssetApply>
    {
        public RevokeAssetApplyValidator()
        {
            ValidateEventId();
            ValidateMessage();
        }
    }
}