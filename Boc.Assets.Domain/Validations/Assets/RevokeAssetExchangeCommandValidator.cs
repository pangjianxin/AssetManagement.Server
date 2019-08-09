using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Validations.Assets
{
    public class RevokeAssetExchangeCommandValidator:AssetCommandValidator<RevokeAssetExchangeCommand>
    {
        public RevokeAssetExchangeCommandValidator()
        {
            ValidateEventId();
            ValidateMessage();
            ValidatePrincipal();
        }
    }
}