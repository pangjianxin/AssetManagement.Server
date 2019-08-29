using Boc.Assets.Domain.Commands.Validations.Organization;
using Boc.Assets.Domain.Core.SharedKernel;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ResetOrgPasswordCommand : OrganizationCommand
    {
        public ResetOrgPasswordCommand(IUser principal, string orgIdentifier) : base(principal)
        {
            OrgIdentifier = orgIdentifier;
        }
        public override bool IsValid()
        {
            ValidationResult = new ResetOrgPasswordCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}