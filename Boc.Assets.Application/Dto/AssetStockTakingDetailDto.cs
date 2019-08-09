using System;

namespace Boc.Assets.Application.Dto
{
    public class AssetStockTakingDetailDto
    {
        /// <summary>
        /// 资产盘点机构登记表外键
        /// </summary>
        public Guid AssetStockTakingOrganizationId { get; set; }
        /// <summary>
        /// 盘点资产外键
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// 责任人员工号
        /// </summary>
        public string ResponsibilityIdentity { get; set; }
        /// <summary>
        /// 责任人名称
        /// </summary>
        public string ResponsibilityName { get; set; }
        /// <summary>
        /// 责任人所在二级机构
        /// </summary>
        public string ResponsibilityOrg2 { get; set; }
        /// <summary>
        /// 资产盘点位置
        /// </summary>
        public string AssetStockTakingLocation { get; set; }


        public string AssetName { get; set; }
        public string AssetDescription { get; set; }
        public string AssetTagNumber { get; set; }
    }
}