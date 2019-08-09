using Boc.Assets.Application.ViewModels.OrganizationSpace;

namespace Boc.Assets.Application.FluentValidations.OrganizationSpace
{
    public class ModifyOrgSpaceInfoValidator : OrgSpaceValidator<ModifySpaceInfo>
    {
        public ModifyOrgSpaceInfoValidator()
        {
            ValidateSpaceId();
            ValidateSpaceName();
            ValidateSpaceDescription(); 
        }
    }
}