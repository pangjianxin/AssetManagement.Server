using Boc.Assets.Application.ViewModels.Assets;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class AssetApplyValidator : AssetValidator<ApplyAsset>
    {
        private void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotNull().NotEmpty().WithMessage("目标机构不能为空");
        }

        public AssetApplyValidator()
        {
            ValidateAssetCategoryId();
            ValidateTargetOrgId();
            ValidateMessage();
        }
    }
}