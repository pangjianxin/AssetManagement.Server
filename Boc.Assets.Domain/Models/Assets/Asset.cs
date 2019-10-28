using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Organizations;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.Assets
{
    public class Asset : EntityBase
    {
        // for ef core
        public Asset()
        {

        }
        /// <summary>
        /// 资产分类外键
        /// </summary>
        public Guid? AssetCategoryId { get; set; }
        /// <summary>
        /// 资产责任中心外键
        /// </summary>
        public Guid? OrganizationInChargeId { get; set; }
        /// <summary>
        /// 资产在用机构外键
        /// </summary>
        public Guid? OrganizationInUseId { get; set; }
        #region Properties
        /// <summary>
        /// 资产名称
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 资产说明,比如设备，可以用来描述设备的配置等信息
        /// </summary>
        public string AssetDescription { get; set; }
        /// <summary>
        /// 资产型号，如有
        /// </summary>
        public string AssetType { get; set; }
        /// <summary>
        /// 资产标签号
        /// </summary>
        public string AssetTagNumber { get; set; }
        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssetNo { get; set; }
        /// <summary>
        /// 资产目前情况
        /// </summary>
        public AssetStatus AssetStatus { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? InStoreDateTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyDateTime { get; set; }
        /// <summary>
        /// 最后一次修改备注
        /// </summary>
        public string LastDeployRecord { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// 资产存放(使用)的机构号
        /// </summary>
        public string OrgInUseIdentifier { get; set; }
        /// <summary>
        /// 资产存放(使用)的机构名称
        /// </summary>
        public string OrgInUseName { get; set; }
        /// <summary>
        /// 资产存放位置
        /// </summary>
        public string AssetLocation { get; set; }
        /// <summary>
        /// 固定资产分类
        /// </summary>
        public virtual AssetCategory AssetCategory { get; set; }
        /// <summary>
        /// 资产管理机构
        /// </summary>
        public virtual Organization OrganizationInCharge { get; set; }
        /// <summary>
        /// 资产在用机构
        /// </summary>
        public virtual Organization OrganizationInUse { get; set; }
        /// <summary>
        /// 对应的资产盘点情况
        /// </summary>
        public virtual ICollection<AssetInventoryDetail> AssetInventoryDetails { get; set; }
        #endregion

        #region methods
        public string ModifyAssetLocation(string assetLocation)
        {
            AssetLocation = assetLocation;
            LastModifyDateTime = DateTime.Now;
            return AssetLocation;
        }

        public void StatusChanged(AssetStatus targetStatus)
        {
            AssetStatus = targetStatus;
        }
        #endregion
    }
    /// <summary>
    /// 资产状态
    /// </summary>
    public enum AssetStatus
    {
        在用 = 1,
        在库 = 2,
        闲置 = 3,
        报废 = 4,
        在途 = 5
    }
}