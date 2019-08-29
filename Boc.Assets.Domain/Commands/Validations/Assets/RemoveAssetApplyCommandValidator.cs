using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class RemoveAssetApplyCommandValidator : AssetCommandValidator<RemoveAssetApplyCommand>
    {
        public RemoveAssetApplyCommandValidator()
        {
            ValidateEventId();
            ValidatePrincipal();
        }
    }
}