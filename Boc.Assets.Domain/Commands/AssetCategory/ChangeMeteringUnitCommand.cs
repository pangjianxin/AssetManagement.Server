using Boc.Assets.Domain.Commands.Validations.AssetCategory;
using Boc.Assets.Domain.Models.Assets;
using System;

namespace Boc.Assets.Domain.Commands.AssetCategory
{
    public class ChangeMeteringUnitCommand : AssetCategoryCommand
    {
        public ChangeMeteringUnitCommand(Guid assetCategoryId, AssetMeteringUnit meteringUnit)
        {
            AssetCategoryId = assetCategoryId;
            AssetMeteringUnit = meteringUnit;
        }

        public override bool IsValid()
        {
            ValidationResult = new ChangeMeteringUnitValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}