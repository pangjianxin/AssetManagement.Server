using Boc.Assets.Domain.Commands.Validations.Organization;
using Boc.Assets.Domain.Core.Commands;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ChangeOrgShortNameCommand : Command<string>
    {
        public string OrgShortNam { get; }
        public string OrgIdentifier { get; }
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