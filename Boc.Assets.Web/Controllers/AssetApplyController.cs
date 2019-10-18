using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Boc.Assets.Web.Controllers
{
    /// <summary>
    /// 资产申请的OData接口
    /// </summary>

    public class AssetApplyController : ODataController
    {
        private readonly IAssetApplyService _assetApplyService;
        private readonly IAssetService _assetService;
        private readonly IUser _user;

        public AssetApplyController(
            IAssetApplyService assetApplyService,
            IAssetService assetService,
            IUser user)
        {
            _assetService = assetService;
            _assetApplyService = assetApplyService;
            _user = user;
        }
        /// <summary>
        /// 资产申请事件分页数据
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetApplyDto> GetRurrent()
        {
            return _assetApplyService.Get(it => it.TargetOrgId == _user.OrgId);
        }
        /// <summary>
        /// 当前机构资产申请事件分页数据
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetApplyDto> GetManage()
        {
            return _assetApplyService.Get(it => it.RequestOrgId == _user.OrgId);
        }
    }
}