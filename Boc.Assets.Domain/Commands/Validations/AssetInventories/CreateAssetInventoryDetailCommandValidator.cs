using Boc.Assets.Domain.Commands.AssetInventory;
using Boc.Assets.Domain.Models.AssetInventories;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Commands.Validations.AssetInventories
{
    public class CreateAssetInventoryDetailCommandValidator : AbstractValidator<CreateAssetInventoryDetailCommand>
    {

        private void ValidateLocation()
        {
            bool CheckLocationByStatus(CreateAssetInventoryDetailCommand command, string location)
            {
                if (command.InventoryStatus != InventoryStatus.盘亏)
                {
                    if (string.IsNullOrEmpty(location) || string.IsNullOrWhiteSpace(location))
                    {
                        return false;
                    }
                }
                return true;
            }
            RuleFor(it => it.AssetInventoryLocation).Must(CheckLocationByStatus).WithMessage("在非盘亏的情况下请输入资产盘点时所处的位置");
        }
        /// <summary>
        /// 检查二级行号
        /// </summary>
        protected void ValidateOrg2()
        {
            RuleFor(it => it.ResponsibilityOrg2).Must(it => !string.IsNullOrEmpty(it) && !string.IsNullOrWhiteSpace(it))
                .WithMessage("责任行二级行号不能为空");
        }
        /// <summary>
        /// 检查资产Id是否为空
        /// </summary>
        protected void ValidateAssetId()
        {
            RuleFor(it => it.AssetId).NotEqual(Guid.Empty).WithMessage("传入的资产序号不能为空");
        }
        /// <summary>
        /// 检查资产盘点参与机构Id是否为空
        /// </summary>
        protected void ValidateAssetInventoryRegisterId()
        {
            RuleFor(it => it.AssetInventoryRegisterId).NotEqual(Guid.Empty).WithMessage("传入的机构号不能为空");
        }
        /// <summary>
        /// 检查责任人工号是否为空
        /// </summary>
        protected void ValidateResponsibilityIdentity()
        {
            RuleFor(it => it.ResponsibilityIdentity).NotNull().NotEmpty().WithMessage("传入的责任人工号不能为空");
        }
        /// <summary>
        /// 检查责任人名称是否为空
        /// </summary>
        protected void ValidateResponsibilityName()
        {
            RuleFor(it => it.ResponsibilityOrg2).NotNull().NotEmpty().WithMessage("传入的二级行机构号不能为空");
        }
        /// <summary>
        /// 检查预留消息是否为空
        /// </summary>
        protected void ValidateMessage()
        {
            RuleFor(it => it.Message).NotNull().NotEmpty().WithMessage("传入的消息不能为空");
        }
        public CreateAssetInventoryDetailCommandValidator()
        {
            ValidateAssetId();
            ValidateAssetInventoryRegisterId();
            // 检查责任人工号
            ValidateResponsibilityIdentity();
            // 检查责任人名称
            ValidateResponsibilityName();
            // 检查消息
            ValidateMessage();
            // 检查二级行号
            ValidateOrg2();
            // 根据资产盘点的状态判断location
            ValidateLocation();

        }
    }
}