using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Assets;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Boc.Assets.Domain.Models.AssetStockTakings
{
    /// <summary>
    /// 资产盘点明细
    /// </summary>
    public class AssetStockTakingDetail : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private AssetStockTakingOrganization _assetStockTakingOrganization;
        private Asset _asset;
        public AssetStockTakingDetail(ILazyLoader lazyLoader = null)
        {
            _lazyLoader = lazyLoader;
        }
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
        /// <summary>
        /// 已被盘点资产
        /// </summary>
        public Asset Asset
        {
            get => _lazyLoader.Load(this, ref _asset);
            set => _asset = value;
        }
        /// <summary>
        /// 资产盘点和机构登记表
        /// </summary>
        public AssetStockTakingOrganization AssetStockTakingOrganization
        {
            get => _lazyLoader.Load(this, ref _assetStockTakingOrganization);
            set => _assetStockTakingOrganization = value;
        }
        public StockTakingStatus StockTakingStatus { get; set; }
    }

    public enum StockTakingStatus
    {
        账面与实物相符 = 0,
        账面与实物不符 = 1,
        盘亏 = 2,
    }
}