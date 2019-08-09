using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.OrganizationSpace
{
    public abstract class OrgSpaceCommand : Command
    {
        protected OrgSpaceCommand(IUser principal) : base(principal)
        {
            Principal = principal;
        }
        public Guid SpaceId { get; set; }
        public string SpaceName { get; set; }
        public string SpaceDescription { get; set; }
    }
}