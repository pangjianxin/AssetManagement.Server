﻿using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.AssetDeploy;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/dashboard")]
    public class DashboardController : ApiController
    {
        private readonly IAssetService _assetService;
        private readonly IAssetDeployService _assetDeployService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public DashboardController(INotificationHandler<DomainNotification> notifications,
            IAssetService assetService,
            IAssetDeployService assetDeployService,
            IUser user,
            IHostingEnvironment hostingEnvironment)
            : base(notifications, user)
        {
            _assetService = assetService;
            _assetDeployService = assetDeployService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("secondaryadmin/assets/categories")]
        [Permission(Permissions.Controllers.Dashboard, Permissions.Actions.Dashboard_Read_Secondary)]
        public async Task<IActionResult> SecondaryAdminAssetCategories()
        {
            var categoriesByThirdLevel = await _assetService.CategoriesByThirdLevelAsync(it => it.OrganizationBelongedId == _user.OrgId);
            var categoriesByStatus = await _assetService.CategoriesByStatusAsync(it => it.OrganizationBelongedId == _user.OrgId);
            return AppResponse(new { categoriesByStatus, categoriesByThirdLevel });
        }

        [HttpGet("current/assets/categories")]
        [Permission(Permissions.Controllers.Dashboard, Permissions.Actions.Dashboard_Read_Current)]
        public async Task<IActionResult> CurrentCategories()
        {
            var categories = await _assetService.CategoriesByThirdLevelAsync(it => it.StoredOrgIdentifier == _user.OrgIdentifier);
            return AppResponse(categories, null);
        }

        [HttpGet("secondaryadmin/assets/pagination")]
        [Permission(Permissions.Controllers.Dashboard, Permissions.Actions.Dashboard_Read_Secondary)]
        public async Task<IActionResult> SecondaryAssetsPaginationAsync(SieveModel model, Guid categoryId, string status = null)
        {
            Expression<Func<Asset, bool>> expression = it => it.OrganizationBelongedId == _user.OrgId;
            if (categoryId != Guid.Empty)
            {
                expression = it =>
                    it.AssetCategoryId == categoryId
                    && it.OrganizationBelongedId == _user.OrgId;

            }

            if (!string.IsNullOrEmpty(status))
            {
                var parseResult = Enum.TryParse(status, out AssetStatus assetStatus);
                if (parseResult)
                {
                    expression = it => it.AssetStatus == assetStatus
                                       && it.OrganizationBelongedId == _user.OrgId
                                       && it.AssetCategoryId == categoryId;
                }
            }
            var pagination = await _assetService.PaginationAsync(model, expression);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        [HttpGet("current/assets/pagination")]
        [Permission(Permissions.Controllers.Dashboard, Permissions.Actions.Dashboard_Read_Current)]
        public async Task<IActionResult> CurrentAssetPagination(SieveModel model, Guid? categoryId = null)
        {
            Expression<Func<Asset, bool>> expression = it => it.StoredOrgIdentifier == _user.OrgIdentifier;
            if (categoryId != null)
            {
                expression = it => it.StoredOrgIdentifier == _user.OrgIdentifier && it.AssetCategoryId == categoryId.Value;
            }
            var pagination = await _assetService.PaginationAsync(model, expression);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        [HttpGet("secondaryadmin/assetdeploy/pagination")]
        [Permission(Permissions.Controllers.Dashboard, Permissions.Actions.Dashboard_Read_Secondary)]
        public async Task<IActionResult> SecondaryAdminAssetDeployPaginationAsync(SieveModel model)
        {
            var pagination = await _assetDeployService.PaginationAsync(model, it => it.AuthorizeOrgInfo.OrgId == _user.OrgId);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }

        [HttpGet("secondaryadmin/assetdeploy/download")]
        [Permission(Permissions.Controllers.Dashboard, Permissions.Actions.Dashboard_Download_Secondary)]
        public async Task<IActionResult> SecondaryAdminDownloadAssetDeploy(DownloadAssetDeploy model)
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