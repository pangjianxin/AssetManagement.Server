using Boc.Assets.Domain.Commands.Validations.Organization;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ChangeOrgShortNameCommand : OrganizationCommand
    {
        public string OrgShortNam { get; set; }
        public ChangeOrgShortNameCommand(string orgIdentifier, string orgShortNam)
        {
            OrgIdentifier = orgIdentifier;
            OrgShortNam = orgShortNam;
        }
        public override bool IsValid()
        {
            ValidationResult = new ChangeOrgShortNameCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}