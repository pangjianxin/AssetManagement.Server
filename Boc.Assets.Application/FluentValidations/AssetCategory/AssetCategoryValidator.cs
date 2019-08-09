using Boc.Assets.Application.ViewModels.AssetCategory;
using FluentValidation;
using System;

namespace Boc.Assets.Application.FluentValidations.AssetCategory
{
    public abstract class AssetCategoryValidator<TModel> : AbstractValidator<TModel> where TModel : AssetCategoryViewModel
    {
        /// <summary>
        /// 检查资产分类Id是否为空
        /// </summary>
        protected void ValidateAssetCategoryId()
        {
            RuleFor(it => it.AssetCategoryId).NotEqual(Guid.Empty).WithMessage("资产分类Id不能为空");
        }
        /// <summary>
        /// 检查资产分类--大类是否为空
        /// </summary>
        protected void ValidateAssetFirstLevelCategory()
        {
            RuleFor(it => it.AssetFirstLevelCategory)
                .NotNull().NotEmpty().WithMessage("资产分类--大类不能为空");
        }
        /// <summary>
        /// 检查资产分类--中类是否为空
        /// </summary>
        protected void ValidateAssetSecondLevelCategory()
        {
            RuleFor(it => it.AssetSecondLevelCategory)
                .NotNull().NotEmpty().WithMessage("资产分类--中类不能为空");
        }
        /// <summary>
        /// 检查资产分类--小类是否为空
        /// </summary>
        protected void ValidateAssetThirdLevelCategory()
        {
            RuleFor(it => it.AssetThirdLevelCategory)
                .NotNull().NotEmpty().WithMessage("资产分类--小类不能为空");
        }
        /// <summary>
        /// 检查计量单位是否为空
        /// </summary>
        protected void ValidateMeteringUnit()
        {
            RuleFor(it => it.AssetMeteringUnit)
                .NotNull().NotEmpty().WithMessage("资产计量单位不能为空");
        }
    }
}