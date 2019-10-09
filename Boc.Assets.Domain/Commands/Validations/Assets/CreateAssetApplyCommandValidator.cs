using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class CreateAssetApplyCommandValidator : ApplyCommandValidator<CreateAssetApplyCommand>
    {
        private void ValidateAssetCategoryId()
        {
            RuleFor(it => it.AssetCategoryId).NotEqual(Guid.Empty).WithMessage("资产分类Id不能为空");
        }
        public CreateAssetApplyCommandValidator()
        {
            ValidateAssetCategoryId();
            ValidateTargetOrgId();
            ValidateMessage();
        }
    }
}