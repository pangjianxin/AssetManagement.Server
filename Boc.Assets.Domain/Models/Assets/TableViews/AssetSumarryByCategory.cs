using System;

namespace Boc.Assets.Domain.Models.Assets.TableViews
{
    public class AssetSumarryByCategory
    {
        public Guid OrganizationInChargeId { get; set; }
        public string OrgInUseIdentifier { get; set; }
        public Guid OrganizationInUseId { get; set; }
        public Guid AssetCategoryId { get; set; }
        public string AssetThirdLevelCategory { get; set; }
        public int AssetCount { get; set; }
    }
}