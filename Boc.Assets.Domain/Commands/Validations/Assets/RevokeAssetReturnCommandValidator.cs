using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class RevokeAssetReturnCommandValidator:ApplyCommandValidator<RevokeAssetReturnCommand>
    {
        public RevokeAssetReturnCommandValidator()
        {
            ValidateApplyId();
            ValidateMessage();
        }
    }
}