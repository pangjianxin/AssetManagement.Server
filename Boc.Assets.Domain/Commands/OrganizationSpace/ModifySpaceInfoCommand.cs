using Boc.Assets.Domain.Commands.Validations.OrganizationSpace;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

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
        public override bool IsValid()
        {
            ValidationResult = new ModifyOrgSpaceInfoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}