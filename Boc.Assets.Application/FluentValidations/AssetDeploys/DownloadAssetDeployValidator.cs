using Boc.Assets.Application.ViewModels.AssetDeploy;
using FluentValidation;
using System;

namespace Boc.Assets.Application.FluentValidations.AssetDeploys
{
    public class DownloadAssetDeployValidator : AbstractValidator<DownloadAssetDeploy>
    {
        private void ValidateStartDate()
        {
            RuleFor(it => it.StartDate).LessThanOrEqualTo(DateTime.Now).WithMessage("起始日期不能大于当前日期");
        }

        private void ValidateEndDate()
        {
            RuleFor(it => it.EndDate).LessThanOrEqualTo(DateTime.Now).WithMessage("结束日期不能大于当前日期");
            RuleFor(it => it.EndDate).Must(EndCannotGreaterThanStart).WithMessage("开始日期不能大于结束日期");
        }

        private bool EndCannotGreaterThanStart(DownloadAssetDeploy model, DateTime endDate)
        {
            return model.StartDate < endDate;
        }

        public DownloadAssetDeployValidator()
        {
            ValidateStartDate();
            ValidateEndDate();
        }
    }
}