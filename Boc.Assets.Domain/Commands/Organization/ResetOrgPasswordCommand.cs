using Boc.Assets.Domain.Commands.Validations.Organization;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ResetOrgPasswordCommand : OrganizationCommand
    {
        public ResetOrgPasswordCommand(string orgIdentifier)
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