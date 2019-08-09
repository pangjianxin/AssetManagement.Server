using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.AssetStockTakings
{
    public class AssetStockTaking : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<AssetStockTakingOrganization> _assetStockTakingOrganizations;
        public AssetStockTaking(ILazyLoader lazyLoader = null)
        {
            _lazyLoader = lazyLoader;
        }

        public AssetStockTaking()
        {

        }

        public AssetStockTaking(Organization organization, string taskName, string taskComment, DateTime expiryDateTime)
        {
            PublisherId = organization.Id;
            PublisherName = organization.OrgNam;
            PublisherIdentifier = organization.OrgIdentifier;
            PublisherOrg2 = organization.Org2;
            ManagementLineId = organization.ManagementLine.Id;
            ManagementLineName = organization.ManagementLine.ManagementLineName;
            ManagementLineDescription = organization.ManagementLine.ManagementLineDescription;
            TaskName = taskName;
            TaskComment = taskComment;
            CreateDateTime = DateTime.Now;
            ExpiryDateTime = expiryDateTime;
            Id = Guid.NewGuid();
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
        /// 盘点任务归属条线外键
        /// </summary>
        public Guid ManagementLineId { get; set; }
        /// <summary>
        /// 盘点任务归属条线的名称
        /// </summary>
        public string ManagementLineName { get; set; }
        /// <summary>
        /// 盘点任务归属条线的描述
        /// </summary>
        public string ManagementLineDescription { get; set; }
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
        public ICollection<AssetStockTakingOrganization> AssetStockTakingOrganizations
        {
            get => _lazyLoader.Load(this, ref _assetStockTakingOrganizations);
            set => _assetStockTakingOrganizations = value;
        }
        #region methods
        public bool IsExpiry()
        {
            return this.ExpiryDateTime <= DateTime.Now;
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