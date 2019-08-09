using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Validations.Assets
{
    public class HandleAssetExchangeCommandValidator : AssetCommandValidator<HandleAssetExchangeCommand>
    {

        public HandleAssetExchangeCommandValidator()
        {
            ValidateEventId();
            ValidatePrincipal();
        }
    }
}