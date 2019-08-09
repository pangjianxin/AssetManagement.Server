using Boc.Assets.Application.ViewModels.Employee;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.Employees
{
    public abstract class EmployeeValidator<TViewModel> : AbstractValidator<TViewModel> where TViewModel : EmployeeViewModel
    {
        protected void ValidateName()
        {
            RuleFor(it => it.Name).NotNull().NotEmpty().WithMessage("员工姓名不能为空");
        }

        protected void ValidateIdentifier()
        {
            RuleFor(it => it.Identifier).NotNull().NotEmpty().WithMessage("员工工号不能为空");
        }
        protected void ValidateTelephone()
        {
            RuleFor(it => it.Telephone).NotNull().NotEmpty().WithMessage("员工手机号不能为空");
        }
        protected void ValidateOfficePhone()
        {
            RuleFor(it => it.OfficePhone).NotNull().NotEmpty().WithMessage("员工办公电话不能为空");
        }
    }
}