using Boc.Assets.Domain.Core.Commands;
using System;

namespace Boc.Assets.Domain.Commands.OrganizationSpace
{
    public abstract class OrgSpaceCommand : Command
    {
        public Guid SpaceId { get; set; }
        public string SpaceName { get; set; }
        public string SpaceDescription { get; set; }
    }
}