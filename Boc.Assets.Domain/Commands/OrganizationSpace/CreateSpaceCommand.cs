using Boc.Assets.Domain.Commands.Validations.OrganizationSpace;

namespace Boc.Assets.Domain.Commands.OrganizationSpace
{
    public class CreateSpaceCommand : OrgSpaceCommand
    {
        public CreateSpaceCommand(string spaceName, string spaceDescription)
        {
            SpaceName = spaceName;
            SpaceDescription = spaceDescription;
        }
        public override bool IsValid()
        {
            ValidationResult = new CreateSpaceCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}