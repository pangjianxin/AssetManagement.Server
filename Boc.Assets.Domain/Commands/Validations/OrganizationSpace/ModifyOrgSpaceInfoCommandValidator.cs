using Boc.Assets.Domain.Commands.OrganizationSpace;

namespace Boc.Assets.Domain.Commands.Validations.OrganizationSpace
{
    public class ModifyOrgSpaceInfoCommandValidator : OrgSpaceCommandValidator<ModifySpaceInfoCommand>
    {
        public ModifyOrgSpaceInfoCommandValidator()
        {
            ValidateSpaceId();
            ValidateSpaceName();
            ValidateSpaceDescription();
 
        }
    }
}