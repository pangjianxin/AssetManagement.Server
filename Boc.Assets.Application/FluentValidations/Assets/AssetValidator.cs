using Boc.Assets.Application.ViewModels.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public abstract class AssetValidator<TViewModel> : AbstractValidator<TViewModel> where TViewModel : AssetViewModel
    {
        protected void ValidateAssetCategoryId()
        {
            RuleFor(it => it.AssetCategoryId).NotNull().NotEmpty().WithMessage("资产分类编号不能为空");
        }
        protected void ValidateAssetId()
        {
            RuleFor(it => it.AssetId).NotEqual(Guid.Empty).WithMessage("资产索引不能为空");
        }
        protected void ValidateAssetName()
        {
            RuleFor(it => it.AssetName).NotNull().NotEmpty().WithMessage("资产名称不能为空");
        }
        protected void ValidateAssetSerialNumber()
        {
            RuleFor(it => it.SerialNumber).NotNull().NotEmpty().WithMessage("资产序列号不能为空");
        }
        protected void ValidateAssetBrand()
        {
            RuleFor(it => it.Brand).NotNull().NotEmpty().WithMessage("资产品牌不能为空");
        }
        protected void ValidateAssetDescription()
        {
            RuleFor(it => it.AssetDescription).NotNull().NotEmpty().WithMessage("资产描述不能为空");
        }
        protected void ValidateAssetType()
        {
            RuleFor(it => it.AssetType).NotNull().NotEmpty().WithMessage("资产型号不能为空");
        }
        protected void ValidateAssetTagNumber()
        {
            RuleFor(it => it.AssetTagNumber).NotNull().NotEmpty().WithMessage("资产标签号不能为空");
        }
        protected void ValidateAssetNo()
        {
            RuleFor(it => it.AssetNo).NotNull().NotEmpty().WithMessage("资产编号不能为空");
        }
        protected void ValidateAssetLocation()
        {
            RuleFor(it => it.AssetLocation).NotNull().NotEmpty().WithMessage("资产位置不能为空");
        }

        protected void ValidateMessage()
        {
            RuleFor(it => it.Message).NotNull().NotEmpty().WithMessage("消息不能为空");
        }

        protected void ValidateEventId()
        {
            RuleFor(it => it.EventId).NotEqual(Guid.Empty).WithMessage("事件Id格式不正确，请联系管理员");
        }

        protected void ValidateCreateDateTime()
        {
            RuleFor(it => it.CreateDateTime).NotNull().NotEmpty().WithMessage("生产日期不能为空");
        }
    }
}