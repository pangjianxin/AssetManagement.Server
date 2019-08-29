using Boc.Assets.Domain.Commands.OrganizationSpace;

namespace Boc.Assets.Domain.Commands.Validations.OrganizationSpace
{
    public class CreateSpaceCommandValidator : OrgSpaceCommandValidator<CreateSpaceCommand>
    {
        public CreateSpaceCommandValidator()
        {
            ValidateSpaceName();
            ValidateSpaceDescription();
        }
    }
}