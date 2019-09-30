using System;
using System.Collections.Generic;
using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Boc.Assets.Domain.Models.AssetInventories
{
    /// <summary>
    /// 参与资产盘点的机构登记表
    /// </summary>
    public class AssetInventoryRegister : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        //资产盘点
        private AssetInventory _assetInventory;
        //参与者
        private Organization _participation;
        //该条登记表关联的资产盘点明细
        private ICollection<AssetInventoryDetail> _assetInventoryDetails;
        public AssetInventoryRegister(ILazyLoader lazyLoader = null)
        {
            _lazyLoader = lazyLoader;
        }
        public AssetInventoryRegister(Guid organizationId, Guid assetInventoryId)
        {
            Id = Guid.NewGuid();
            ParticipationId = organizationId;
            AssetInventoryId = assetInventoryId;
        }
        /// <summary>
        /// 资产盘点参与机构外键
        /// </summary>
        public Guid ParticipationId { get; set; }
        /// <summary>
        /// 归属资产盘点任务外键
        /// </summary>
        public Guid AssetInventoryId { get; set; }
        /// <summary>
        /// 资产盘点任务
        /// </summary>
        public AssetInventory AssetInventory
        {
            get => _lazyLoader.Load(this, ref _assetInventory);
            set => _assetInventory = value;
        }
        /// <summary>
        /// 资产盘点任务参与机构
        /// </summary>
        public Organization Participation
        {
            get => _lazyLoader.Load(this, ref _participation);
            set => _participation = value;
        }
        /// <summary>
        /// 已盘点的资产明细
        /// </summary>
        public ICollection<AssetInventoryDetail> AssetInventoryDetails
        {
            get => _lazyLoader.Load(this, ref _assetInventoryDetails);
            set => _assetInventoryDetails = value;
        }

        #region methods

        public string Progress()
        {
            if (Participation.AssetsInUse.Count > 0)
            {
                return $"{Math.Round((double)AssetInventoryDetails.Count / Participation.AssetsInUse.Count * 100, 2) }";
            }
            return $"0";
        }

        #endregion
    }
}