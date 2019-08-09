using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Validations.Assets
{
    public class ExchangeAssetCommandValidator : AssetCommandValidator<ExchangeAssetCommand>
    {
        private void ValidateExchangeOrgId()
        {
            RuleFor(it => it.ExchangeOrgId).NotEqual(Guid.Empty).WithMessage("调换机构不能为空");
        }
        private void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotEqual(Guid.Empty).WithMessage("审核机构不能为空");
        }

        public ExchangeAssetCommandValidator()
        {
            ValidateMessage();
            ValidateExchangeOrgId();
            ValidateTargetOrgId();
            ValidateAssetId();
            ValidatePrincipal();
        }
    }
}