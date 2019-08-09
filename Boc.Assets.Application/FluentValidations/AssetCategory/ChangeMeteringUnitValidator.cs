using Boc.Assets.Application.ViewModels.AssetCategory;

namespace Boc.Assets.Application.FluentValidations.AssetCategory
{
    public class ChangeMeteringUnitValidator : AssetCategoryValidator<ChangeMeteringUnit>
    {
        public ChangeMeteringUnitValidator()
        {
            ValidateAssetCategoryId();
            ValidateMeteringUnit();
        }
    }
}