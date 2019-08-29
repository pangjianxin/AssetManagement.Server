using Boc.Assets.Domain.Commands.Validations.Employees;
using Boc.Assets.Domain.Core.SharedKernel;

namespace Boc.Assets.Domain.Commands.Employee
{
    public class AddEmployeeCommand : EmployeeCommand
    {
        public AddEmployeeCommand(IUser principal,
            string name, string identifier, string org2, string telephone, string officePhone) : base(principal)
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