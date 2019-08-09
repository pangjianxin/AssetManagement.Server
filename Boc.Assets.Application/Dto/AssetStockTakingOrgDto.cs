using System;

namespace Boc.Assets.Application.Dto
{
    public class AssetStockTakingOrgDto
    {
        public Guid Id { get; set; }
        public Guid OrgId { get; set; }
        public Guid AssetStockTakingId { get; set; }
        public string OrgIdentifier { get; set; }
        public string OrgNam { get; set; }
        public string Org2 { get; set; }
        public string TaskName { get; set; }
        public string TaskComment { get; set; }
        public string Progress { get; set; }

    }
}