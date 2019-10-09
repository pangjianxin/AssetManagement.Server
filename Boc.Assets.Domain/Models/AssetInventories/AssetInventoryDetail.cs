using System;
using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Assets;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Boc.Assets.Domain.Models.AssetInventories
{
    /// <summary>
    /// 资产盘点明细
    /// </summary>
    public class AssetInventoryDetail : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private AssetInventoryRegister _assetInventoryRegister;
        private Asset _asset;
        public AssetInventoryDetail(ILazyLoader lazyLoader = null)
        {
            _lazyLoader = lazyLoader;
        }
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
        public AssetInventoryRegister AssetInventoryRegister
        {
            get => _lazyLoader.Load(this, ref _assetInventoryRegister);
            set => _assetInventoryRegister = value;
        }
        public InventoryStatus InventoryStatus { get; set; }
    }
    /// <summary>
    /// 资产盘点状态
    /// </summary>
    public enum InventoryStatus
    {
        账面与实物相符 = 1,
        账面与实物不符 = 2,
        盘亏 = 3,
    }
}