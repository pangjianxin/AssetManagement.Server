using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using System;
using Boc.Assets.Domain.Commands.Validations.AssetCategory;

namespace Boc.Assets.Domain.Commands.AssetCategory
{
    public class ChangeMeteringUnitCommand : AssetCategoryCommand
    {
        public ChangeMeteringUnitCommand(IUser principal, Guid assetCategoryId, AssetMeteringUnit meteringUnit) : base(principal)
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