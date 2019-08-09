using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.OrganizationSpace;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.OrganizationSpace
{
    public class ModifySpaceInfoCommand : OrgSpaceCommand
    {
        public ModifySpaceInfoCommand(
            IUser principal,
            Guid spaceId,
            string spaceName,
            string spaceDescription) : base(principal)
        {
            SpaceId = spaceId;
            SpaceName = spaceName;
            SpaceDescription = spaceDescription;
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ModifyOrgSpaceInfoCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}