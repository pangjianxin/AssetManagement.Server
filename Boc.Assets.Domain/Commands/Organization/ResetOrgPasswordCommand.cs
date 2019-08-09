using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Organization;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ResetOrgPasswordCommand : OrganizationCommand
    {
        public ResetOrgPasswordCommand(IUser principal, string orgIdentifier) : base(principal)
        {
            OrgIdentifier = orgIdentifier;
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ResetOrgPasswordCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}