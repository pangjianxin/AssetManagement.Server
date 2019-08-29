using Boc.Assets.Domain.Commands.Validations.Employees;

namespace Boc.Assets.Domain.Commands.Employee
{
    public class AddEmployeeCommand : EmployeeCommand
    {
        public AddEmployeeCommand(string name, string identifier, string org2, string telephone, string officePhone)
        {
            Name = name;
            Identifier = identifier;
            Org2 = org2;
            Telephone = telephone;
            OfficePhone = officePhone;
        }
        public override bool IsValid()
        {
            ValidationResult = new AddEmployeeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}