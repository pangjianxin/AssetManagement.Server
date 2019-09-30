using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.Assets
{
    public class Asset : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;

        public Asset(ILazyLoader lazyLoader = null)
        {
            _lazyLoader = lazyLoader;
        }
        private AssetCategory _assetCategory;
        private Organization _organizationInCharge;
        private Organization _organizationInUse;
        private ICollection<AssetInventoryDetail> _assetInventoryDetails;
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
        public AssetCategory AssetCategory
        {
            get => _lazyLoader.Load(this, ref _assetCategory);
            set => _assetCategory = value;
        }
        /// <summary>
        /// 资产管理机构
        /// </summary>
        public Organization OrganizationInCharge
        {
            get => _lazyLoader.Load(this, ref _organizationInCharge);
            set => _organizationInCharge = value;
        }
        /// <summary>
        /// 资产在用机构
        /// </summary>
        public Organization OrganizationInUse
        {
            get => _lazyLoader.Load(this, ref _organizationInUse);
            set => _organizationInUse = value;
        }
        /// <summary>
        /// 对应的资产盘点情况
        /// </summary>
        public ICollection<AssetInventoryDetail> AssetInventoryDetails
        {
            get => _lazyLoader.Load(this, ref _assetInventoryDetails);
            set => _assetInventoryDetails = value;
        }
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
        在用 = 0,
        在库 = 1,
        闲置 = 2,
        报废 = 3,
        在途 = 4
    }
}