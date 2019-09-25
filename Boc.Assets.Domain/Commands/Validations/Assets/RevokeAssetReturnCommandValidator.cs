using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class RevokeAssetReturnCommandValidator:AssetCommandValidator<RevokeAssetReturnCommand>
    {
        public RevokeAssetReturnCommandValidator()
        {
            ValidateEventId();
            ValidateMessage();
        }
    }
}