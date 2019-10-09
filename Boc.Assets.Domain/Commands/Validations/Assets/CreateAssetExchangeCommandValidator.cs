using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class CreateAssetExchangeCommandValidator : ApplyCommandValidator<CreateAssetExchangeCommand>
    {
        public CreateAssetExchangeCommandValidator()
        {
            ValidateMessage();
            ValidateExchangeOrgId();
            ValidateTargetOrgId();
            ValidateAssetId();
        }
    }
}