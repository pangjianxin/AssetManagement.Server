using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class RevokeAssetApplyCommandValidator:ApplyCommandValidator<RevokeAssetApplyCommand>
    {
        public RevokeAssetApplyCommandValidator()
        {
            ValidateApplyId();
            ValidateMessage();
        } 
    }
}