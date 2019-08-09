using Boc.Assets.Domain.Commands.ManagementLine;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Validations.ManagementLine
{
    public abstract class ManagementLineCommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : ManagementLineCommand
    {
        protected void ValidateManagementLineId()
        {
            RuleFor(it => it.ManagementLineId).NotNull().NotEmpty().NotEqual(Guid.Empty).WithMessage("条线的标识不能为空");
        }

        protected void ValidateManagementLineName()
        {
            RuleFor(it => it.ManagementLineName).NotNull().NotEmpty().WithMessage("条线名称不能为空");
        }

        protected void ValidateManagementLineDescription()
        {
            RuleFor(it => it.ManagementLineDescription).NotNull().NotEmpty().WithMessage("条线描述不能为空");
        }

        protected void ValidateCreateDateTime()
        {
            RuleFor(it => it.CreateDateTime).NotNull().NotEmpty().WithMessage("创建时间不能为空");
        }
        protected void ValidatePrincipal()
        {
            RuleFor(it => it.Principal).NotNull().WithMessage("主体不能为空");
        }
    }
}