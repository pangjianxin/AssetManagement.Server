using Boc.Assets.Domain.Commands.AssetStockTaking;

namespace Boc.Assets.Domain.Validations.AssetStockTakings
{
    public class CreateAssetStockTakingCommandValidator : AssetStocktakingCommandValidator<CreateAssetStockTakingCommand>
    {
        public CreateAssetStockTakingCommandValidator()
        {
            ValidateTaskName();
            ValidateTaskComment();
            ValidateExpiryDate();
            ValidatePrincipal();
        }
    }
}