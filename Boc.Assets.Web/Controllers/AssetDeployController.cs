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
    public class AssetDeployController : ODataController
    {
        private readonly IAssetDeployService _assetDeployService;
        private readonly IUser _user;
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public AssetDeployController(IAssetDeployService deployService, IUser user)
        {
            _assetDeployService = deployService;
            _user = user;
        }
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetDeployDto> Get()
        {
            Expression<Func<AssetDeploy, bool>> predicate = it => it.AuthorizeOrgInfo.OrgId == _user.OrgId;
            return _assetDeployService.Get(predicate);
        }
    }
}