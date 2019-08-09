using Boc.Assets.Application.ViewModels.OrganizationSpace;

namespace Boc.Assets.Application.FluentValidations.OrganizationSpace
{
    public class CreateOrgSpaceValidator : OrgSpaceValidator<CreateSpace>
    {
        public CreateOrgSpaceValidator()
        {
            ValidateSpaceName();
            ValidateSpaceDescription();
        }
    }
}