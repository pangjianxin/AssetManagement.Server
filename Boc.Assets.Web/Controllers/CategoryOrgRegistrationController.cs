using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/categoryOrgRegistration")]
    public class CategoryOrgRegistrationController : ApiController
    {
        private readonly ICategoryManageResgisterService _categoryOrgRegistrationService;

        public CategoryOrgRegistrationController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            ICategoryManageResgisterService categoryOrgRegistrationService)
            : base(notifications, user)
        {
            _categoryOrgRegistrationService = categoryOrgRegistrationService;
        }
        [HttpGet("examinations")]
        public async Task<IActionResult> GetExaminationOrganizations(Guid categoryId)
        {
            var organizations = await _categoryOrgRegistrationService.GetOrgByCategoryAndOrg2(categoryId, _user.Org2);
            return AppResponse(organizations);
        }
    }
}