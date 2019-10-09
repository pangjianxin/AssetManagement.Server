using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class HandleAssetReturnCommandValidator : ApplyCommandValidator<HandleAssetReturnCommand>
    {
        public HandleAssetReturnCommandValidator()
        {
            ValidateApplyId();
        }
    }
}