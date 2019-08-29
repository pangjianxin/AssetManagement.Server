using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
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