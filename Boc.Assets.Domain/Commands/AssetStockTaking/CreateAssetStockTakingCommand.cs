using Boc.Assets.Domain.Commands.Validations.AssetStockTakings;
using Boc.Assets.Domain.Core.SharedKernel;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Commands.AssetStockTaking
{
    /// <summary>
    /// 二级行管理部门创建一个资产盘点任务
    /// </summary>
    public class CreateAssetStockTakingCommand : AssetStockTakingCommand
    {
        public CreateAssetStockTakingCommand(IUser principal,
            string taskName,
            string taskComment,
            DateTime expiryDateTime,
            IEnumerable<Guid> excludedOrganizations) : base(principal)
        {
            TaskName = taskName;
            TaskComment = taskComment;
            ExpiryDateTime = expiryDateTime;
            ExcludedOrganizations = excludedOrganizations;
        }
        public IEnumerable<Guid> ExcludedOrganizations { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CreateAssetStockTakingCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}