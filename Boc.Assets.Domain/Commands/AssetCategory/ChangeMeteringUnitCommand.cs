using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Validations.AssetCategory;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.AssetCategory
{
    public class ChangeMeteringUnitCommand : AssetCategoryCommand
    {
        public ChangeMeteringUnitCommand(IUser principal, Guid assetCategoryId, AssetMeteringUnit meteringUnit) : base(principal)
        {
            AssetCategoryId = assetCategoryId;
            AssetMeteringUnit = meteringUnit;
        }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ChangeMeteringUnitValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}