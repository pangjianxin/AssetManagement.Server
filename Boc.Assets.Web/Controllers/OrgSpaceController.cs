using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Web.Controllers
{
    public class OrgSpaceController : ODataController
    {
        private readonly IOrgSpaceService _orgSpaceService;
        private readonly IUser _user;
        public OrgSpaceController(
            IUser user,
            IOrgSpaceService orgSpaceService)
        {
            _orgSpaceService = orgSpaceService;
            _user = user;
        }
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<OrgSpaceDto> Get()
        {
            Expression<Func<OrganizationSpace, bool>> predicate = it => it.OrgId == _user.OrgId;
            return _orgSpaceService.Get(predicate);
        }
    }
}