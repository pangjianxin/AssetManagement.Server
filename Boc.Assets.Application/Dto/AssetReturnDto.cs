using System;

namespace Boc.Assets.Application.Dto
{
    public class AssetReturnDto
    {
        public Guid EventId { get; set; }
        public Guid AssetId { get; set; }
        public string AssetName { get; set; }
        public Guid RequestOrgId { get; set; }
        public string RequestOrgIdentifier { get; set; }
        public string RequestOrgNam { get; set; }
        public string Org2 { get; set; }
        public Guid TargetOrgId { get; set; }
        public string TargetOrgIdentifier { get; set; }
        public string TargetOrgNam { get; set; }
        public Guid AggregateId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string DateTimeFromNow { get; set; }
    }
}