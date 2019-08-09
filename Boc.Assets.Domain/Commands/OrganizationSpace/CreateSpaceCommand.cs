using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.OrganizationSpace;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.OrganizationSpace
{
    public class CreateSpaceCommand : OrgSpaceCommand
    {
        public CreateSpaceCommand(IUser principal, string spaceName, string spaceDescription) : base(principal)
        {
            SpaceName = spaceName;
            SpaceDescription = spaceDescription;
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new CreateSpaceCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}