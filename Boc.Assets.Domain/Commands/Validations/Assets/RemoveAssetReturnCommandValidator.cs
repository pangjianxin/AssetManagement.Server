using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class RemoveAssetReturnCommandValidator : ApplyCommandValidator<RemoveAssetReturnCommand>
    {
        public RemoveAssetReturnCommandValidator()
        {
            ValidateApplyId();
        }
    }
}