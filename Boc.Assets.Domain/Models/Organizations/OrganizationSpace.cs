using Boc.Assets.Domain.Core.Models;
using System;

namespace Boc.Assets.Domain.Models.Organizations
{
    public class OrganizationSpace : EntityBase
    {
        public OrganizationSpace() { }

        public Guid OrgId { get; set; }
        public string OrgIdentifier { get; set; }
        public string OrgName { get; set; }
        public virtual Organization Organization { get; set; }
        public string SpaceName { get; set; }
        public string SpaceDescription { get; set; }
        #region methods

        public void ModifySpaceInfo(string spaceName, string spaceDescription)
        {
            SpaceName = spaceName;
            SpaceDescription = spaceDescription;
        }
        #endregion  
    }
}