using Boc.Assets.Domain.Commands.AssetInventory;

namespace Boc.Assets.Domain.Commands.Validations.AssetInventories
{
    public class CreateAssetInventoryCommandValidator : AssetInventoryComnandValidator<CreateAssetInventoryCommand>
    {
        public CreateAssetInventoryCommandValidator()
        {
            ValidateTaskName();
            ValidateTaskComment();
            ValidateExpiryDate();
        }
    }
}