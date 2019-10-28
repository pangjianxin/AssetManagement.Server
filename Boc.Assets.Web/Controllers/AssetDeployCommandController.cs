using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.AssetDeploy;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/assetDeploy")]
    public class AssetDeployCommandController : ApiController
    {
        private readonly IAssetDeployService _assetDeployService;
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public AssetDeployCommandController(IAssetDeployService deployService,
            IUser user,
            INotificationHandler<DomainNotification> notifications) : base(notifications, user)
        {
            _assetDeployService = deployService;
        }
        /// <summary>
        /// 下载报表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("download")]
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