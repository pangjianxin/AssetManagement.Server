using System;
using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class CreateAssetReturnCommandValidator : AssetCommandValidator<CreateAssetReturnCommand>
    {
        private void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotEqual(Guid.Empty).WithMessage("目标机构Id不能为空");
        }
        public CreateAssetReturnCommandValidator()
        {
            ValidateTargetOrgId();
            ValidateAssetId();
            ValidateMessage();
        }
    }
}