using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.AssetStockTakings;
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
        private Organization _organizationBelonged;
        private ICollection<AssetStockTakingDetail> _assetStockTakingDetails;
        /// <summary>
        /// 资产分类外键
        /// </summary>
        public Guid AssetCategoryId { get; set; }
        /// <summary>
        /// 资产责任中心外键
        /// </summary>
        public Guid? OrganizationBelongedId { get; set; }
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
        public string LastModifyComment { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// 资产存放的机构号
        /// </summary>
        public string StoredOrgIdentifier { get; set; }
        /// <summary>
        /// 资产存放的机构名称
        /// </summary>
        public string StoredOrgName { get; set; }
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
        public Organization OrganizationBelonged
        {
            get => _lazyLoader.Load(this, ref _organizationBelonged);
            set => _organizationBelonged = value;
        }

        public ICollection<AssetStockTakingDetail> AssetStockTakingDetails
        {
            get => _lazyLoader.Load(this, ref _assetStockTakingDetails);
            set => _assetStockTakingDetails = value;
        }
        #endregion

        #region methods

        public void ModifyAssetLocation(string assetLocation)
        {
            AssetLocation = assetLocation;
            LastModifyDateTime = DateTime.Now;
            LastModifyComment = "资产修改了存放位置";
        }

        public void ModifyAssetStatus(AssetStatus targetStatus)
        {
            LastModifyDateTime = DateTime.Now;
            LastModifyComment = $"资产状态由{AssetStatus.ToString()}变更为{targetStatus.ToString()}";
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