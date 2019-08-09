using System;

namespace Boc.Assets.Application.Dto
{
    public class OrgSpaceDto
    {
        public Guid SpaceId { get; set; }
        public string SpaceName { get; set; }
        public string SpaceDescription { get; set; }
        public Guid OrgId { get; set; }
        public string OrgIdentifier { get; set; }
        public string OrgName { get; set; }
    }
}