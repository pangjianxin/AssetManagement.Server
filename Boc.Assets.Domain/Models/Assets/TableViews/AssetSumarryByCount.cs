using System;

namespace Boc.Assets.Domain.Models.Assets.TableViews
{
    public class AssetSumarryByCount
    {
        public string OrgInUseIdentifier { get; set; }
        public Guid OrganizationInUseId { get; set; }
        public Guid OrganizationInChargeId { get; set; }
        public int AssetCount { get; set; }
    }
}