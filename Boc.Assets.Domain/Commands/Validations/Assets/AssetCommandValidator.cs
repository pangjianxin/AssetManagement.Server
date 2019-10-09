using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public abstract class AssetCommandValidator<TCommand>
        : AbstractValidator<TCommand> where TCommand : AssetCommand
    {
        protected void ValidateAssetId()
        {
            RuleFor(it => it.AssetId).NotEqual(Guid.Empty).WithMessage("资产索引不能为空");
        }

        protected void ValidateAssetCategoryId()
        {
            RuleFor(it => it.AssetCategoryId).NotNull().NotEmpty().WithMessage("资产分类编号不能为空");
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
        protected void ValidateCreateDateTime()
        {
            RuleFor(it => it.CreateDateTime).NotEqual(DateTime.Today).NotEmpty().NotNull().WithMessage("生产日期错误");
        }
    }
}