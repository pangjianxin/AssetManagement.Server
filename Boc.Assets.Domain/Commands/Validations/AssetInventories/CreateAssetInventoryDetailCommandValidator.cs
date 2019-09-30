using System;
using Boc.Assets.Domain.Commands.AssetInventory;
using FluentValidation;

namespace Boc.Assets.Domain.Commands.Validations.AssetInventories
{
    public class CreateAssetInventoryDetailCommandValidator : AssetInventoryComnandValidator<CreateAssetInventoryDetailCommand>
    {
        /// <summary>
        /// 检查资产Id是否为空
        /// </summary>
        private void ValidateAssetId()
        {
            RuleFor(it => it.AssetId).NotEqual(Guid.Empty).WithMessage("传入的资产序号不能为空");
        }
        /// <summary>
        /// 检查资产盘点参与机构Id是否为空
        /// </summary>
        private void ValidateAssetInventoryRegisterId()
        {
            RuleFor(it => it.AssetInventoryRegisterId).NotEqual(Guid.Empty).WithMessage("传入的机构号不能为空");
        }
        /// <summary>
        /// 检查责任人工号是否为空
        /// </summary>
        private void ValidateResponsibilityIdentity()
        {
            RuleFor(it => it.ResponsibilityIdentity).NotNull().NotEmpty().WithMessage("传入的责任人工号不能为空");
        }
        /// <summary>
        /// 检查责任人名称是否为空
        /// </summary>
        private void ValidateResponsibilityName()
        {
            RuleFor(it => it.ResponsibilityOrg2).NotNull().NotEmpty().WithMessage("传入的二级行机构号不能为空");
        }
        /// <summary>
        /// 检查预留消息是否为空
        /// </summary>
        private void ValidateMessage()
        {
            RuleFor(it => it.Message).NotNull().NotEmpty().WithMessage("传入的消息不能为空");
        }
        public CreateAssetInventoryDetailCommandValidator()
        {
            ValidateAssetId();
            ValidateAssetInventoryRegisterId();
            ValidateResponsibilityIdentity();
            ValidateResponsibilityName();
        }
    }
}