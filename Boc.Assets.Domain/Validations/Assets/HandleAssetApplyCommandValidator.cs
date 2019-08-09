using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Validations.Assets
{
    public class HandleAssetApplyCommandValidator : AssetCommandValidator<HandleAssetApplyCommand>
    {
        public HandleAssetApplyCommandValidator()
        {
            ValidateEventId();
            ValidateAssetId();
            ValidatePrincipal();
        }
    }
}