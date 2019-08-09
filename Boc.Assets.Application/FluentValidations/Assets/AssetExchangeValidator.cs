using Boc.Assets.Application.ViewModels.Assets;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class AssetExchangeValidator : AssetValidator<ExchangeAsset>
    {
        private void ValidateExchangeOrgId()
        {
            RuleFor(it => it.ExchangeOrgId).NotNull().NotEmpty().WithMessage("调换机构不能为空");
        }

        private void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotNull().NotEmpty().WithMessage("审核机构不能为空");
        }
        public AssetExchangeValidator()
        {
            ValidateExchangeOrgId();
            ValidateTargetOrgId();
            ValidateMessage();
            ValidateAssetId();
        }
    }
}