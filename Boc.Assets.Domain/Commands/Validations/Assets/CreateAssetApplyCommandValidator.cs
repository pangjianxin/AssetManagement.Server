using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class CreateAssetApplyCommandValidator : AssetCommandValidator<CreateAssetApplyCommand>
    {
        private void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotEqual(Guid.Empty).WithMessage("目标机构序号不能为空");
        }
        public CreateAssetApplyCommandValidator()
        {
            ValidateAssetCategoryId();
            ValidateTargetOrgId();
            ValidateMessage();
        }
    }
}