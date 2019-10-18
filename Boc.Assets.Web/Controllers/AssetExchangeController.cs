using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Boc.Assets.Web.Controllers
{
    public class AssetExchangeController : ODataController
    {
        private readonly IAssetExchangeService _assetExchangeService;
        private readonly IUser _user;

        public AssetExchangeController(
            IUser user,
            IAssetExchangeService assetExchangeService)
        {
            _user = user;
            _assetExchangeService = assetExchangeService;
        }
        /// <summary>
        /// 资产调配分页数据
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetExchangeDto> GetManage()
        {
            return _assetExchangeService.Get(it => it.TargetOrgId == _user.OrgId);
        }
        /// <summary>
        /// 资产调配分页数据
        ///  当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetExchangeDto> GetCurrent()
        {
            return _assetExchangeService.Get(it => it.RequestOrgId == _user.OrgId);

        }
    }
}