using Boc.Assets.Application.ViewModels.Maintainers;
using FluentValidation;
using System;

namespace Boc.Assets.Application.FluentValidations.Maintainers
{
    public abstract class MaintainersValidator<TViewModel> : AbstractValidator<TViewModel> where TViewModel : MaintainerViewModel
    {
        protected void ValidateCompanyName()
        {
            RuleFor(it => it.CompanyName).NotNull().NotEmpty().WithMessage("公司名称不能为空");
        }
        protected void ValidateMaintainerName()
        {
            RuleFor(it => it.MaintainerName).NotNull().NotEmpty().WithMessage("维修工人名称不能为空");
        }
        protected void ValidateTelephone()
        {
            RuleFor(it => it.Telephone).NotNull().NotEmpty().WithMessage("手机号不能为空").Must(it => it.Length == 11).WithMessage("手机号码长度错误");
        }

        protected void ValidateCategoryId()
        {
            RuleFor(it => it.AssetCategoryId).NotEqual(Guid.Empty).WithMessage("关联资产分类信息不能为空(Guid.Empty)");
        }

        protected void ValidateMaintainerId()
        {
            RuleFor(it => it.MaintainerId).NotEqual(Guid.Empty).WithMessage("服务商ID不能为空");
        }
    }
}