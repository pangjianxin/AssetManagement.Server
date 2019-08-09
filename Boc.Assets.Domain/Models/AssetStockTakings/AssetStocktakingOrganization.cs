using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.AssetStockTakings
{
    /// <summary>
    /// 参与资产盘点的机构登记表
    /// </summary>
    public class AssetStockTakingOrganization : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private AssetStockTaking _assetStockTaking;
        private Organization _organization;
        private ICollection<AssetStockTakingDetail> _assetStockTakingDetails;
        public AssetStockTakingOrganization(ILazyLoader lazyLoader = null)
        {
            _lazyLoader = lazyLoader;
        }
        /// <summary>
        /// only for ef core
        /// </summary>
        public AssetStockTakingOrganization()
        {

        }

        public AssetStockTakingOrganization(Guid organizationId, Guid assetStockTakingId)
        {
            Id = Guid.NewGuid();
            OrganizationId = organizationId;
            AssetStockTakingId = assetStockTakingId;
        }
        /// <summary>
        /// 资产盘点参与机构外键
        /// </summary>
        public Guid OrganizationId { get; set; }
        /// <summary>
        /// 归属资产盘点任务外键
        /// </summary>
        public Guid AssetStockTakingId { get; set; }
        /// <summary>
        /// 资产盘点任务
        /// </summary>
        public AssetStockTaking AssetStockTaking
        {
            get => _lazyLoader.Load(this, ref _assetStockTaking);
            set => _assetStockTaking = value;
        }
        /// <summary>
        /// 资产盘点任务参与机构
        /// </summary>
        public Organization Organization
        {
            get => _lazyLoader.Load(this, ref _organization);
            set => _organization = value;
        }
        /// <summary>
        /// 已盘点的资产明细
        /// </summary>
        public ICollection<AssetStockTakingDetail> AssetStockTakingDetails
        {
            get => _lazyLoader.Load(this, ref _assetStockTakingDetails);
            set => _assetStockTakingDetails = value;
        }

        #region methods

        public string Progress()
        {
            if (Organization.Assets.Count > 0)
            {
                return $"{Math.Round((double)AssetStockTakingDetails.Count / Organization.Assets.Count * 100, 2) }";
            }
            return $"0";
        }

        #endregion
    }
}