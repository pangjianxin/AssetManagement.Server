using Boc.Assets.Domain.Commands.AssetCategory;

namespace Boc.Assets.Domain.Commands.Validations.AssetCategory
{
    public class ChangeMeteringUnitValidator : AssetCategoryCommandValidator<ChangeMeteringUnitCommand>
    {

        public ChangeMeteringUnitValidator()
        {
            ValidateAssetCategoryId();
            ValidateMeteringUnit();
        }
    }
}