using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Employees;
using System.Threading.Tasks;

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
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new AddEmployeeCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}