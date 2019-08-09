using System;

namespace Boc.Assets.Application.Dto
{
    public class AssetDeployDto
    {
        public string AssetDeployCategory { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string DateTimeFromNow { get; set; }
        public string Org2 { get; set; }
        public Guid AssetId { get; set; }
        public string AssetNo { get; set; }
        public string AssetTagNumber { get; set; }
        public string AssetName { get; set; }
        public string ExportOrgIdentifier { get; set; }
        public string ExportOrgNam { get; set; }
        public string ImportOrgIdentifier { get; set; }
        public string ImportOrgNam { get; set; }
        public string AuthorizeOrgIdentifier { get; set; }
        public string AuthorizeOrgNam { get; set; }

    }
}