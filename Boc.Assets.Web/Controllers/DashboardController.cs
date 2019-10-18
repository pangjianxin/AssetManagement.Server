using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.AssetDeploy;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Authorize(Policy = "manage")]
    public class DashboardController : ODataController
    {
        private readonly IAssetService _assetService;
        private readonly IAssetDeployService _assetDeployService;
        private readonly IUser _user;
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public DashboardController(
            IAssetService assetService,
            IAssetDeployService assetDeployService,
            IUser user)
        {
            _assetService = assetService;
            _assetDeployService = assetDeployService;
            _user = user;
        }

        [EnableQuery]
        public async Task<IActionResult> GetManageAssetsCategories()
        {
            var categoriesByThirdLevel = await _assetService.CategoriesByThirdLevelAsync(it => it.OrganizationInChargeId == _user.OrgId);
            var categoriesByStatus = await _assetService.CategoriesByStatusAsync(it => it.OrganizationInChargeId == _user.OrgId);
            return Ok(new { categoriesByStatus, categoriesByThirdLevel });
        }
        [EnableQuery]
        public IQueryable<AssetDto> GetManageAsests(Guid categoryId, string status = null)
        {
            Expression<Func<Asset, bool>> predicate = it => it.OrganizationInChargeId == _user.OrgId;
            if (categoryId != Guid.Empty)
            {
                predicate = it =>
                    it.AssetCategoryId == categoryId
                    && it.OrganizationInChargeId == _user.OrgId;

            }

            if (!string.IsNullOrEmpty(status))
            {
                var parseResult = Enum.TryParse(status, out AssetStatus assetStatus);
                if (parseResult)
                {
                    predicate = it => it.AssetStatus == assetStatus
                                       && it.OrganizationInChargeId == _user.OrgId
                                       && it.AssetCategoryId == categoryId;
                }
            }

            return _assetService.Get(predicate);
        }
        /// <summary>
        /// 查询该机构审批过的所有资产流转记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public IQueryable<AssetDeployDto> GetDeploys()
        {
            Expression<Func<AssetDeploy, bool>> predicate = it => it.AuthorizeOrgInfo.OrgId == _user.OrgId;
            return _assetDeployService.Get(predicate);
        }
        /// <summary>
        /// 下载报表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnableQuery]
        [Authorize(Policy = "manage")]
        public async Task<IActionResult> DownLoad(DownloadAssetDeploy model)
        {
            byte[] reportBytes;
            using (var package = await _assetDeployService.DownloadAssetDeploy(model))
            {
                reportBytes = package.GetAsByteArray();
            }

            return File(reportBytes, XlsxContentType, "report.xlsx");
        }
    }
}