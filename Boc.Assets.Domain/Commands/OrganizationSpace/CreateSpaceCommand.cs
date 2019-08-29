using Boc.Assets.Domain.Commands.Validations.OrganizationSpace;
using Boc.Assets.Domain.Core.SharedKernel;

namespace Boc.Assets.Domain.Commands.OrganizationSpace
{
    public class CreateSpaceCommand : OrgSpaceCommand
    {
        public CreateSpaceCommand(IUser principal, string spaceName, string spaceDescription) : base(principal)
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