using Boc.Assets.Application.ViewModels.ManagementLines;
using FluentValidation;
using System;

namespace Boc.Assets.Application.FluentValidations.ManagementLines
{
    public class ManagementLineValidator<TViewModel> : AbstractValidator<TViewModel> where TViewModel : ManagementLineViewModel
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
    }
}