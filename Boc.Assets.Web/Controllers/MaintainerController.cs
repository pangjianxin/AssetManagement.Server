using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Web.Controllers
{
    public class MaintainerController : ODataController
    {
        private readonly IMaintainerService _maintainerService;
        private readonly IUser _user;

        public MaintainerController(
            IUser user,
            IMaintainerService maintainerService)

        {
            _maintainerService = maintainerService;
            _user = user;
        }
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<MaintainerDto> GetCurrent()
        {
            Expression<Func<Maintainer, bool>> predicate = it => it.OrganizationId == _user.OrgId;
            return _maintainerService.Get(predicate);
        }

        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<MaintainerDto> GetManage()
        {
            Expression<Func<Maintainer, bool>> predicate = it => it.Org2 == _user.Org2;
            return _maintainerService.Get(predicate);
        }
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<MaintainerDto> GetByCategoryId([FromODataUri]Guid assetCategoryId)
        {
            Expression<Func<Maintainer, bool>> predicate = it =>
                it.AssetCategoryId == assetCategoryId && it.Org2 == _user.Org2;
            return _maintainerService.Get(predicate);
        }
    }
}