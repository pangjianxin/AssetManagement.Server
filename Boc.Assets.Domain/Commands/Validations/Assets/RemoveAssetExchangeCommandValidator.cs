using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class RemoveAssetExchangeCommandValidator : AssetCommandValidator<RemoveAssetExchangeCommand>
    {
        public RemoveAssetExchangeCommandValidator()
        {
            ValidateEventId();
        }
    }
}