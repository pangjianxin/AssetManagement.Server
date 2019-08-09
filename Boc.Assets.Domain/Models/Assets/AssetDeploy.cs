using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Models.Assets
{
    public class AssetDeploy : EntityBase
    {
        public AssetDeployCategory AssetDeployCategory { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 调配资产标签号
        /// </summary>
        public string AssetTagNumber { get; set; }
        /// <summary>
        /// 调配资产名称
        /// </summary>
        public string AssetName { get; set; }
        public OrganizationInfo ExportOrgInfo { get; set; }
        public OrganizationInfo ImportOrgInfo { get; set; }
        public OrganizationInfo AuthorizeOrgInfo { get; set; }
        #region methods
        /// <summary>
        /// 显示事件发生对比目前的时间戳
        /// </summary>
        /// <returns></returns>
        public string DateTimeFromNow
        {
            get
            {
                var span = DateTime.Now - CreateDateTime;
                if (span.TotalDays > 60)
                {
                    return "2个月前";
                }

                if (span.TotalDays > 30)
                {
                    return "1个月前";
                }

                if (span.TotalDays > 14)
                {
                    return "2周前";
                }

                if (span.TotalDays > 7)
                {
                    return "1周前";
                }

                if (span.TotalDays > 1)
                {
                    return $"{(int)Math.Floor(span.TotalDays)}天前";
                }

                if (span.TotalHours > 1)
                {
                    return $"{(int)Math.Floor(span.TotalHours)}小时前";
                }

                if (span.TotalMinutes > 1)
                {
                    return $"{(int)Math.Floor(span.TotalMinutes)}分钟前";
                }

                if (span.TotalSeconds >= 1)
                {
                    return $"{(int)Math.Floor(span.TotalSeconds)}秒前";
                }
                return "1秒前";
            }
        }
        #endregion
    }
    public enum AssetDeployCategory
    {
        资产申请 = 0,
        资产交回 = 1,
        资产机构间调配 = 2
    }

    public class OrganizationInfo : ValueObject
    {
        public string Org2 { get; set; }
        public Guid OrgId { get; set; }
        public string OrgIdentifier { get; set; }
        public string OrgNam { get; set; }
    }
}