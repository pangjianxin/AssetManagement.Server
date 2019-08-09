using System;

namespace Boc.Assets.Application.Dto
{
    public class NonAuditEventDto
    {
        public Guid Id { get; set; }
        public string OrgIdentifier { get; set; }
        public string OrgNam { get; set; }
        public string Org2 { get; set; }
        public string Type { get; set; }
        public string DateTimeFromNow { get; set; }
    }
}