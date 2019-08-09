using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Validations.Assets
{
    public class RevokeAssetApplyCommandValidator:AssetCommandValidator<RevokeAssetApplyCommand>
    {
        public RevokeAssetApplyCommandValidator()
        {
            ValidateEventId();
            ValidateMessage();
            ValidatePrincipal();
        } 
    }
}