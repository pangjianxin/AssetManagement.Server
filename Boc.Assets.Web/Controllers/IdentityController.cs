using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Authentication;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Web.Controllers
{
    public class IdentityController : ODataController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IJwtFactory _jwtFactory;
        private readonly IUser _user;

        public IdentityController(
            IUser user,
            IOrganizationService organizationService,
            IJwtFactory jwtFactory)
        {
            _organizationService = organizationService;
            _jwtFactory = jwtFactory;
            _user = user;
        }
        /// <summary>
        /// 获取前二十个搜索结果
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<OrgDto> Get(string searchInput)
        {
            Expression<Func<Organization, bool>> predicate = it =>
                it.Org2 == _user.Org2
                && (it.OrgNam.Contains(searchInput)
                || it.OrgIdentifier.Contains(searchInput));
            return _organizationService.Get(predicate);
        }
        /// <summary>
        /// 获取某个二级行下面的所有机构
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<OrgDto> Get()
        {
            Expression<Func<Organization, bool>> predicate = it => it.Org2 == _user.Org2;
            return _organizationService.Get(predicate);
        }
    }
}
