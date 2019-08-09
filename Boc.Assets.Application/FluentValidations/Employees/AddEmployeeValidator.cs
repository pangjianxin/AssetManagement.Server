using Boc.Assets.Application.ViewModels.Employee;

namespace Boc.Assets.Application.FluentValidations.Employees
{
    public class AddEmployeeValidator : EmployeeValidator<AddEmployee>
    {
        public AddEmployeeValidator()
        {
            ValidateName();
            ValidateIdentifier();
        }
    }
}