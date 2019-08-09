using Boc.Assets.Application.ViewModels.Assets;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class AssetReturnValidator : AssetValidator<ReturnAsset>
    {
        private void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotNull().NotEmpty().WithMessage("目标机构不能为空");
        }


        public AssetReturnValidator()
        {
            ValidateTargetOrgId();
            ValidateMessage();
            ValidateAssetId();
        }
    }
}