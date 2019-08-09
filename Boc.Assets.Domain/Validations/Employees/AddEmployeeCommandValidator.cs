using Boc.Assets.Domain.Commands.Employee;

namespace Boc.Assets.Domain.Validations.Employees
{
    public class AddEmployeeCommandValidator : EmployeeCommandValidators<AddEmployeeCommand>
    {
        public AddEmployeeCommandValidator()
        {
            ValidateName();
            ValidateIdentifier();
            ValidateOrg2();
            ValidatePrincipal();
        }
    }
}