using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class CreateAssetReturnCommandValidator : ApplyCommandValidator<CreateAssetReturnCommand>
    {
        public CreateAssetReturnCommandValidator()
        {
            ValidateTargetOrgId();
            ValidateAssetId();
            ValidateMessage();
        }
    }
}