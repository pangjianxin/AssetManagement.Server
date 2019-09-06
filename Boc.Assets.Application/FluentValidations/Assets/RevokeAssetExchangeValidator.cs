using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class RevokeAssetExchangeValidator:AssetValidator<RevokeAssetExchange>
    {
        public RevokeAssetExchangeValidator()
        {
            ValidateEventId();
            ValidateMessage();
        }
    }
}