using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class HandleAssetExchangeCommandValidator : ApplyCommandValidator<HandleAssetExchangeCommand>
    {
        public HandleAssetExchangeCommandValidator()
        {
            ValidateApplyId();
        }
    }
}