using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class CreateAssetExchangeCommandValidator : AssetCommandValidator<CreateAssetExchangeCommand>
    {
        private void ValidateExchangeOrgId()
        {
            RuleFor(it => it.ExchangeOrgId).NotEqual(Guid.Empty).WithMessage("调换机构不能为空");
        }
        private void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotEqual(Guid.Empty).WithMessage("审核机构不能为空");
        }

        public CreateAssetExchangeCommandValidator()
        {
            ValidateMessage();
            ValidateExchangeOrgId();
            ValidateTargetOrgId();
            ValidateAssetId();
        }
    }
}