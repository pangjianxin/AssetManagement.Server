using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    public class CategoryManagerRegisterController : ODataController
    {
        private readonly ICategoryManageResgisterService _categoryOrgRegistrationService;
        private readonly IUser _user;
        public CategoryManagerRegisterController(
            IUser user,
            ICategoryManageResgisterService categoryOrgRegistrationService)

        {
            _user = user;
            _categoryOrgRegistrationService = categoryOrgRegistrationService;
        }
        [EnableQuery]
        [Authorize(Policy = "user")]
        public async Task<IEnumerable<OrgDto>> GetOrgByCategory([FromODataUri]Guid categoryId)
        {
            var organizations = await _categoryOrgRegistrationService.GetOrgByCategoryAndOrg2(categoryId, _user.Org2);
            return organizations;
        }
    }
}