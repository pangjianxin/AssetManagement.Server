using Boc.Assets.Domain.Commands.AssetCategory;
using FluentValidation;

namespace Boc.Assets.Domain.Validations.AssetCategory
{
    public abstract class AssetCategoryCommandValidator<TEntity>
        : AbstractValidator<TEntity> where TEntity : AssetCategoryCommand
    {
        /// <summary>
        /// 检查资产分类Id是否为空
        /// </summary>
        protected virtual void ValidateAssetCategoryId()
        {
            RuleFor(it => it.AssetCategoryId).NotNull().NotEmpty().WithMessage("资产分类Id不能为空");
        }
        /// <summary>
        /// 检查资产分类--大类是否为空
        /// </summary>
        protected virtual void ValidateAssetFirstLevelCategory()
        {
            RuleFor(it => it.AssetFirstLevelCategory)
                .NotNull().NotEmpty().WithMessage("资产分类--大类不能为空");
        }
        /// <summary>
        /// 检查资产分类--中类是否为空
        /// </summary>
        protected virtual void ValidateAssetSecondLevelCategory()
        {
            RuleFor(it => it.AssetSecondLevelCategory)
                .NotNull().NotEmpty().WithMessage("资产分类--中类不能为空");
        }
        /// <summary>
        /// 检查资产分类--小类是否为空
        /// </summary>
        protected virtual void ValidateAssetThirdLevelCategory()
        {
            RuleFor(it => it.AssetThirdLevelCategory)
                .NotNull().NotEmpty().WithMessage("资产分类--小类不能为空");
        }
        /// <summary>
        /// 检查计量单位是否为空
        /// </summary>
        protected virtual void ValidateMeteringUnit()
        {
            RuleFor(it => it.AssetMeteringUnit)
                .NotNull().NotEmpty().WithMessage("资产分类--大类不能为空");
        }
        protected void ValidatePrincipal()
        {
            RuleFor(it => it.Principal).NotNull().WithMessage("主体不能为空");
        }
    }
}