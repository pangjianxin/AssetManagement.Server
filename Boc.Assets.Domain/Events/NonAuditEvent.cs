using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Events
{/// <summary>
 /// 谁操作的，操作的谁，操作的什么事件
 /// </summary>
    public class NonAuditEvent : Event
    {
        //for ef core
        public NonAuditEvent() { }
        public NonAuditEvent(IUser principal, NonAuditEventType eventType)
        {
            Type = eventType;
            OrgId = principal.OrgId;
            OrgIdentifier = principal.OrgIdentifier;
            OrgNam = principal.OrgNam;
            Org2 = principal.Org2;
        }
        public Guid OrgId { get; protected set; }
        public string OrgIdentifier { get; protected set; }
        public string OrgNam { get; protected set; }
        public string Org2 { get; set; }
        public NonAuditEventType Type { get; private set; }

        public override string ToString()
        {
            return $"{OrgIdentifier}:{Type.ToString()}";
        }
    }
    /// <summary>
    /// 非审计类事件类型
    /// </summary>
    public enum NonAuditEventType
    {
        机构密码重置 = 0,
        机构简称变更 = 1,
        机构密码变更 = 2,
        资产分类计量单位变更 = 3,
        新增机构空间 = 4,
        机构空间名称或描述变更 = 5,
        资产存放位置信息变更 = 6,
        机构添加员工 = 7
    }
}