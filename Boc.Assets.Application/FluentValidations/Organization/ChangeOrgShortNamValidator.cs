using Boc.Assets.Application.ViewModels.Organization;

namespace Boc.Assets.Application.FluentValidations.Organization
{
    public class ChangeOrgShortNamValidator : OrganizationValidator<ChangeOrgShortName>
    {
        private void ValidateOrgShortNam()
        {
            RuleFor(it => it.OrgShortNam);
        }

        public ChangeOrgShortNamValidator()
        {
            ValidateOrgIdentifier();
            ValidateOrgShortNam();
        }
    }
}