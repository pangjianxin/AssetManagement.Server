using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;

namespace Boc.Assets.Domain.Validations.Assets
{
    public class StoreAssetWithOutFileCommandValidator : AssetCommandValidator<StoreAssetWithOutFileCommand>
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

        private void ValidateRequestOrgId()
        {

        }
        public StoreAssetWithOutFileCommandValidator()
        {
            ValidateAssetName();
            ValidateAssetBrand();
            ValidateAssetDescription();
            ValidateAssetType();
            ValidateAssetCategoryId();
            ValidateAssetLocation();
            ValidateCreateDateTime();
            ValidateTagNumber();
            ValidatePrincipal();
        }
    }
}