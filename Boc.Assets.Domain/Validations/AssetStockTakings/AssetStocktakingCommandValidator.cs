using Boc.Assets.Domain.Commands.AssetStockTaking;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Validations.AssetStockTakings
{
    public class AssetStocktakingCommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : AssetStockTakingCommand
    {

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
        protected void ValidatePrincipal()
        {
            RuleFor(it => it.Principal).NotNull().WithMessage("主体不能为空");
        }
    }
}