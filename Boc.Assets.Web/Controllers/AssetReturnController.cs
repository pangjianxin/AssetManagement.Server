using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Applies;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Web.Controllers
{
    public class AssetReturnController : ODataController
    {
        private readonly IAssetReturnService _assetReturnService;
        private readonly IUser _user;

        public AssetReturnController(
            IUser user,
            IAssetReturnService assetReturnService)
        {
            _user = user;
            _assetReturnService = assetReturnService;
        }
        /// <summary>
        /// 前五条数据
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetReturnDto> GetManage()
        {
            Expression<Func<AssetReturn, bool>> predicate = it => it.TargetOrgId == _user.OrgId;
            return _assetReturnService.Get(predicate);
        }
        /// <summary>
        /// 资产交回分页数据
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetReturnDto> GetCurrent()
        {
            Expression<Func<AssetReturn, bool>> predicate = it => it.RequestOrgId == _user.OrgId;
            return _assetReturnService.Get(predicate);
        }
    }
}