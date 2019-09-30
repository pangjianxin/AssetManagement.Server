using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.ValueObjects;
using System;
using System.Text;

namespace Boc.Assets.Domain.Models
{
    /// <summary>
    /// 表示所有申请的基类
    /// </summary>
    public class RequestEntity : EntityBase
    {
        protected RequestEntity()
        {

        }
        protected RequestEntity(OrganizationInfo principal, OrganizationInfo targetOrg, string message)
        {
            RequestOrgId = principal.OrgId;
            RequestOrgIdentifier = principal.OrgIdentifier;
            RequestOrgNam = principal.OrgNam;
            TargetOrgId = targetOrg.OrgId;
            TargetOrgIdentifier = targetOrg.OrgIdentifier;
            TargetOrgNam = targetOrg.OrgNam;
            Message = message;
            Status = AuditEntityStatus.待处理;
            TimeStamp = DateTime.Now;
        }
        /// <summary>
        /// 请求主体Id
        /// </summary>
        public Guid RequestOrgId { get; protected set; }
        /// <summary>
        /// 请求主体标识（机构号）
        /// </summary>
        public string RequestOrgIdentifier { get; protected set; }
        /// <summary>
        /// 请求主体的名称（机构名称）
        /// </summary>
        public string RequestOrgNam { get; protected set; }
        /// <summary>
        /// 审批机构Id
        /// </summary>
        public Guid TargetOrgId { get; protected set; }
        /// <summary>
        /// 审批机构标识（机构号）
        /// </summary>
        public string TargetOrgIdentifier { get; protected set; }
        /// <summary>
        /// 审批机构名称
        /// </summary>
        public string TargetOrgNam { get; protected set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime TimeStamp { get; protected set; }
        /// <summary>
        /// 状态
        /// </summary>
        public AuditEntityStatus Status { get; protected set; }
        /// <summary>
        /// 请求主体预留消息
        /// </summary>
        public string Message { get; protected set; }
        /// <summary>
        /// 最后一次修改备注
        /// </summary>
        public string LastModifiedContent { get; protected set; }


        #region methods

        public void Complete(string message)
        {
            LastModifiedContent = message;
            Status = AuditEntityStatus.已完成;
        }

        public void Revoke(string message)
        {
            LastModifiedContent = message;
            Status = AuditEntityStatus.已撤销;
        }

        public virtual string DateTimeFromNow()
        {
            DateTime current = DateTime.Now;
            var span = current.Subtract(TimeStamp);
            var timeStrBuilder = new StringBuilder();
            if (span.Days > 0)
            {
                timeStrBuilder.Append($"{span.Days}天");
            }

            if (span.Hours > 0)
            {
                timeStrBuilder.Append($"{span.Hours}小时");
            }

            if (span.Minutes > 0)
            {
                timeStrBuilder.Append($"{span.Minutes}分钟");
            }

            if (span.Seconds > 0)
            {
                timeStrBuilder.Append($"{span.Seconds}秒");
            }

            return timeStrBuilder.ToString();
        }
        #endregion
    }
    /// <summary>
    /// 审计类事件状态
    /// </summary>
    public enum AuditEntityStatus
    {
        已完成 = 0,
        待处理 = 1,
        已撤销 = 2,
        处理中 = 3
    }
}