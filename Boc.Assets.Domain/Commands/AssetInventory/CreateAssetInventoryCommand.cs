using System;
using System.Collections.Generic;
using Boc.Assets.Domain.Commands.Validations.AssetInventories;

namespace Boc.Assets.Domain.Commands.AssetInventory
{
    /// <summary>
    /// 二级行管理部门创建一个资产盘点任务
    /// </summary>
    public class CreateAssetInventoryCommand : AssetInventoryCommand
    {
        public CreateAssetInventoryCommand(
            string taskName,
            string taskComment,
            DateTime expiryDateTime,
            IEnumerable<Guid> excludedOrganizations)
        {
            TaskName = taskName;
            TaskComment = taskComment;
            ExpiryDateTime = expiryDateTime;
            ExcludedOrganizations = excludedOrganizations;
        }
        public IEnumerable<Guid> ExcludedOrganizations { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CreateAssetInventoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}