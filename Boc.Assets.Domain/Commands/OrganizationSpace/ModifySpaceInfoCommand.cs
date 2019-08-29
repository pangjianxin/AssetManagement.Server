using Boc.Assets.Domain.Commands.Validations.OrganizationSpace;
using System;

namespace Boc.Assets.Domain.Commands.OrganizationSpace
{
    public class ModifySpaceInfoCommand : OrgSpaceCommand
    {
        public ModifySpaceInfoCommand(
            Guid spaceId,
            string spaceName,
            string spaceDescription)
        {
            SpaceId = spaceId;
            SpaceName = spaceName;
            SpaceDescription = spaceDescription;
        }
        public override bool IsValid()
        {
            ValidationResult = new ModifyOrgSpaceInfoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}