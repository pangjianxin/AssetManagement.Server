using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using System;

namespace Boc.Assets.Domain.Models
{
    public class AuditEntity : EntityBase
    {
        public Guid RequestOrgId { get; protected set; }
        public string RequestOrgIdentifier { get; protected set; }
        public string RequestOrgNam { get; protected set; }
        public string Org2 { get; protected set; }
        public Guid TargetOrgId { get; protected set; }
        public string TargetOrgIdentifier { get; protected set; }
        public string TargetOrgNam { get; protected set; }
        public DateTime TimeStamp { get; protected set; }
        public AuditEntityStatus Status { get; protected set; }
        public string Message { get; protected set; }
        protected AuditEntity() { }
        protected AuditEntity(IUser principal, Organization targetOrg, string message)
        {
            RequestOrgId = principal.OrgId;
            RequestOrgIdentifier = principal.OrgIdentifier;
            RequestOrgNam = principal.OrgNam;
            Org2 = principal.Org2;
            TargetOrgId = targetOrg.Id;
            TargetOrgIdentifier = targetOrg.OrgIdentifier;
            TargetOrgNam = targetOrg.OrgNam;
            Message = message;
            Status = AuditEntityStatus.待处理;
            TimeStamp = DateTime.Now;
        }
        #region methods

        public void Complete()
        {
            Status = AuditEntityStatus.已完成;
        }

        public void Revoke(string message)
        {
            Status = AuditEntityStatus.已撤销;
            Message = message;
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