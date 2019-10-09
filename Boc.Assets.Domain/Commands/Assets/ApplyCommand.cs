using Boc.Assets.Domain.Core.Commands;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public abstract class ApplyCommand : Command
    {
        /// <summary>
        /// 资产Id
        /// </summary>
        public Guid AssetId { get; protected set; }
        /// <summary>
        /// 调配机构Id
        /// </summary>
        public Guid ExchangeOrgId { get; protected set; }
        /// <summary>
        /// 目标机构Id(一般情况下是审核机构)
        /// </summary>
        public Guid TargetOrgId { get; protected set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; protected set; }
        /// <summary>
        /// 申请Id
        /// </summary>
        public Guid ApplyId { get; protected set; }
    }
}