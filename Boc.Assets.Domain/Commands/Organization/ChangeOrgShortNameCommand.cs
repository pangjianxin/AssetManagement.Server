using Boc.Assets.Domain.Commands.Validations.Organization;
using Boc.Assets.Domain.Core.SharedKernel;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ChangeOrgShortNameCommand : OrganizationCommand
    {
        public string OrgShortNam { get; set; }
        public ChangeOrgShortNameCommand(IUser principal, string orgIdentifier, string orgShortNam) : base(principal)
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