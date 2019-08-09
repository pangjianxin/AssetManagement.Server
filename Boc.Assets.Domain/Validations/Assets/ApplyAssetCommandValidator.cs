using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Validations.Assets
{
    public class ApplyAssetCommandValidator : AssetCommandValidator<ApplyAssetCommand>
    {
        private void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotEqual(Guid.Empty).WithMessage("目标机构序号不能为空");
        }
        public ApplyAssetCommandValidator()
        {
            ValidateAssetCategoryId();
            ValidateTargetOrgId();
            ValidateMessage();
            ValidatePrincipal();
        }
    }
}