using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Organizations;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.AssetInventories
{
    /// <summary>
    /// 参与资产盘点的机构登记表
    /// </summary>
    public class AssetInventoryRegister : EntityBase
    {
        //该条登记表关联的资产盘点明细

        public AssetInventoryRegister()
        {
        }
        public AssetInventoryRegister(Guid organizationId, Guid assetInventoryId)
        {
            Id = Guid.NewGuid();
            ParticipationId = organizationId;
            AssetInventoryId = assetInventoryId;
            CreateDateTime = DateTime.Now;
        }

        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 资产盘点参与机构外键
        /// </summary>
        public virtual Guid ParticipationId { get; set; }
        /// <summary>
        /// 归属资产盘点任务外键
        /// </summary>
        public virtual Guid AssetInventoryId { get; set; }
        /// <summary>
        /// 资产盘点任务
        /// </summary>
        public virtual AssetInventory AssetInventory { get; set; }
        /// <summary>
        /// 资产盘点任务参与机构
        /// </summary>
        public virtual Organization Participation { get; set; }
        /// <summary>
        /// 已盘点的资产明细
        /// </summary>
        public virtual ICollection<AssetInventoryDetail> AssetInventoryDetails { get; set; }

        #region methods
        /// <summary>
        /// 查询进度
        /// </summary>
        /// <returns></returns>
        public string Progress()
        {
            //if (Participation.AssetsInUse.Count > 0)
            //{
            //    return $"{Math.Round((double)AssetInventoryDetails.Count / Participation.AssetsInUse.Count * 100, 2) }";
            //}

            return $"0";
        }

        #endregion
    }
}