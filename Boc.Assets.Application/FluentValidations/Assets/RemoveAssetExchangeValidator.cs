using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class RemoveAssetExchangeValidator : AssetValidator<RemoveAssetExchange>
    {
        public RemoveAssetExchangeValidator()
        {
            ValidateEventId();
        }
    }
}