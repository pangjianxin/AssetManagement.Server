using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace Boc.Assets.Web.Controllers
{
    public class AssetCategoryController : ODataController
    {
        private readonly IAssetCategoryService _assetCategoryService;
        private readonly IUser _user;

        public AssetCategoryController(
            IAssetCategoryService assetCategoryService,
            IUser user)
        {
            _assetCategoryService = assetCategoryService;
            _user = user;
        }
        /// <summary>
        /// 资产分类分页
        /// 二级权限
        /// 后期考虑对该api进行合并
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetCategoryDto> GetManage()
        {
            return _assetCategoryService.Get(it => it.CategoryManageRegisters.Select(that => that.ManagerId).Contains(_user.OrgId));
        }
        /// <summary>
        /// 资产分类分页数据
        /// 当前用户权限
        /// 后期考虑对该api进行合并
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<AssetCategoryDto> GetCurrent()
        {
            return _assetCategoryService.Get();

        }
        /// <summary>
        /// 资产分类计量单位
        /// 当前机构权限
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IEnumerable<dynamic> GetMeteringUnits()
        {
            return _assetCategoryService.GetMeteringUnits();
        }

    }
}