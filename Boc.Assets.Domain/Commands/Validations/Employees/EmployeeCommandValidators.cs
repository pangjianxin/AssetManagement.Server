using Boc.Assets.Domain.Commands.Employee;
using FluentValidation;

namespace Boc.Assets.Domain.Commands.Validations.Employees
{
    public abstract class EmployeeCommandValidators<TCommand> : AbstractValidator<TCommand> where TCommand : EmployeeCommand
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

        protected void ValidateOrg2()
        {
            RuleFor(it => it.Org2).NotNull().NotEmpty().WithMessage("二级行机构号必须输入");
        }
    }
}