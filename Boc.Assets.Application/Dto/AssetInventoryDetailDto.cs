using System;

namespace Boc.Assets.Application.Dto
{
    public class AssetInventoryDetailDto
    {
        /// <summary>
        /// 资产盘点机构登记表外键
        /// </summary>
        public Guid AssetInventoryRegisterId { get; set; }
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
        public string AssetInventoryLocation { get; set; }
        /// <summary>
        /// 资产名称
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// 资产描述
        /// </summary>
        public string AssetDescription { get; set; }
        /// <summary>
        /// 资产标签号
        /// </summary>
        public string AssetTagNumber { get; set; }
        /// <summary>
        /// 资产盘点状态
        /// </summary>
        public string InventoryStatus { get; set; }
    }
}