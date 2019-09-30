using System;
using Boc.Assets.Application.ViewModels.AssetInventories;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.AssetInventories
{
    public abstract class AssetInventoryValidator<TViewmodel> : AbstractValidator<TViewmodel>
        where TViewmodel : AssetInventoryViewModel
    {
        protected void ValidatePublisherId()
        {
            RuleFor(it => it.PublisherId).NotNull().NotEmpty().NotEqual(Guid.Empty).WithMessage("发布机构信息有误，请联系管理员");
        }

        protected void ValidateTaskName()
        {
            RuleFor(it => it.TaskName).NotNull().NotEmpty().WithMessage("盘点任务名称不能为空");
        }

        protected void ValidateTaskComment()
        {
            RuleFor(it => it.TaskComment).NotNull().NotEmpty().WithMessage("盘点任务备注不能为空");
        }

        protected void ValidateExpiryDate()
        {
            RuleFor(it => it.ExpiryDateTime).NotNull().NotEmpty().WithMessage("过期时间不能为空");
            RuleFor(it => it.ExpiryDateTime).Must(date => DateTime.Now < date).WithMessage("资产盘点任务过期时间不能小于当前系统时间");
        }
    }
}