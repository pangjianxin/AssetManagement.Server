using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class ApplyCommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : ApplyCommand
    {
        protected void ValidateAssetId()
        {
            RuleFor(it => it.AssetId).NotEqual(Guid.Empty).WithMessage("资产ID不能为空");
        }

        protected void ValidateTargetOrgId()
        {
            RuleFor(it => it.TargetOrgId).NotEqual(Guid.Empty).WithMessage("目标机构Id不能为空");
        }

        protected void ValidateExchangeOrgId()
        {
            RuleFor(it => it.ExchangeOrgId).NotEqual(Guid.Empty).WithMessage("调换机构Id不能为空");
        }

        protected void ValidateMessage()
        {
            RuleFor(it => it.Message).Must(it => !string.IsNullOrEmpty(it) && !string.IsNullOrWhiteSpace(it))
                .WithMessage("消息不能为空");
        }

        protected void ValidateApplyId()
        {
            RuleFor(it => it.ApplyId).NotEqual(Guid.Empty).WithMessage("申请Id不能为空");
        }
    }
}