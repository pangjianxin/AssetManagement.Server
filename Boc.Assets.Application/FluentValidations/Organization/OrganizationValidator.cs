using Boc.Assets.Application.ViewModels.Organization;
using FluentValidation;
using System;

namespace Boc.Assets.Application.FluentValidations.Organization
{
    public abstract class OrganizationValidator<TViewModel>
        : AbstractValidator<TViewModel> where TViewModel : OrganizationViewModel
    {
        protected void ValidateOrganizationId()
        {
            RuleFor(it => it.OrganizationId).NotNull().NotEmpty().NotEqual(Guid.Empty).WithMessage("机构索引不能为空");
        }
        protected void ValidateOrgIdentifier()
        {
            RuleFor(it => it.OrgIdentifier).NotEmpty().NotNull().WithMessage("机构号不能为空");
        }

        protected void ValidateOrgNam()
        {
            RuleFor(it => it.OrgNam).NotEmpty().NotNull().WithMessage("机构名称不能为空");
        }
    }
}