using System;
using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class ReturnAssetCommandValidator : AssetCommandValidator<ReturnAssetCommand>
    {
        private void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotEqual(Guid.Empty).WithMessage("目标机构Id不能为空");
        }
        public ReturnAssetCommandValidator()
        {
            ValidateTargetOrgId();
            ValidateAssetId();
            ValidateMessage();
            ValidatePrincipal();
        }
    }
}