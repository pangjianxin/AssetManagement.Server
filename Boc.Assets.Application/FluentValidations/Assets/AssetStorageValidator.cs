using Boc.Assets.Application.ViewModels.Assets;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class AssetStorageValidator : AssetValidator<StoreAsset>
    {
        private void ValidateTagNumber()
        {
            RuleFor(it => it.StartTagNumber).Length(15).NotNull().NotEmpty().WithMessage("开始标签号长度错误");
            RuleFor(it => it.EndTagNumber).Length(15).NotNull().NotEmpty().WithMessage("结束标签号长度错误");
            RuleFor(it => it.StartTagNumber).Must((model, startTagNumber) =>
            {
                var start = startTagNumber.Substring(0, 10);
                var end = model.EndTagNumber.Substring(0, 10);
                return start == end;
            }).WithMessage("开始标签号与结束标签号的格式不一致");
        }
        public AssetStorageValidator()
        {
            ValidateAssetName();
            ValidateAssetBrand();
            ValidateAssetDescription();
            ValidateAssetType();
            ValidateAssetLocation();
            ValidateAssetCategoryId();
            ValidateCreateDateTime();
            ValidateTagNumber();
        }
    }
}