using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class RevokeAsstReturnValidator:AssetValidator<RevokeAssetReturn>
    {
        public RevokeAsstReturnValidator()
        {
            ValidateEventId();
            ValidateMessage();
        }
    }
}