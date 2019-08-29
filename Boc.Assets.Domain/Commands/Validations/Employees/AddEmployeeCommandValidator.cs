using Boc.Assets.Domain.Commands.Employee;

namespace Boc.Assets.Domain.Commands.Validations.Employees
{
    public class AddEmployeeCommandValidator : EmployeeCommandValidators<AddEmployeeCommand>
    {
        public AddEmployeeCommandValidator()
        {
            ValidateName();
            ValidateIdentifier();
            ValidateOrg2();
        }
    }
}