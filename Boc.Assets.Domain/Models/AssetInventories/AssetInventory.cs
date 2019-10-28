using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Organizations;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.AssetInventories
{
    /// <summary>
    /// 资产盘点
    /// </summary>
    public class AssetInventory : EntityBase
    {
        public AssetInventory() { }
        public AssetInventory(Organization organization,
            string taskName,
            string taskComment,
            DateTime expiryDateTime)
        {
            Id = Guid.NewGuid();
            PublisherId = organization.Id;
            PublisherName = organization.OrgNam;
            PublisherIdentifier = organization.OrgIdentifier;
            PublisherOrg2 = organization.Org2;
            TaskName = taskName;
            TaskComment = taskComment;
            CreateDateTime = DateTime.Now;
            ExpiryDateTime = expiryDateTime;
        }
        /// <summary>
        /// 盘点任务发布机构号
        /// </summary>
        public Guid PublisherId { get; set; }
        /// <summary>
        /// 盘点任务发布人名称
        /// </summary>
        public string PublisherName { get; set; }
        /// <summary>
        /// 盘点任务发布人的机构号
        /// </summary>
        public string PublisherIdentifier { get; set; }
        /// <summary>
        /// 盘点任务发布人的二级机构号
        /// </summary>
        public string PublisherOrg2 { get; set; }
        /// <summary>
        /// 盘点任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 盘点任务描述/备注
        /// </summary>
        public string TaskComment { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiryDateTime { get; set; }
        public virtual ICollection<AssetInventoryRegister> AssetInventoryRegisters { get; set; }
        #region methods
        public bool IsExpiry()
        {
            return ExpiryDateTime <= DateTime.Now;
        }
        /// <summary>
        /// 时间进度
        /// </summary>
        /// <returns></returns>
        public string TimeProgress()
        {
            return IsExpiry() ? $"100" : $"{Math.Round((DateTime.Now - CreateDateTime) / (ExpiryDateTime - CreateDateTime) * 100, 2)}";
        }
        #endregion
    }
}