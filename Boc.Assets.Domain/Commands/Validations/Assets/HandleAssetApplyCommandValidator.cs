using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class HandleAssetApplyCommandValidator : ApplyCommandValidator<HandleAssetApplyCommand>
    {
        public HandleAssetApplyCommandValidator()
        {
            ValidateApplyId();
            ValidateAssetId();
        }
    }
}