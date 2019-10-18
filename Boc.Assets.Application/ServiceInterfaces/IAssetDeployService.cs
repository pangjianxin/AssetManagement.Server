using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.AssetDeploy;
using Boc.Assets.Domain.Models.Assets;
using OfficeOpenXml;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetDeployService : IApplicationService
    {

        IQueryable<AssetDeployDto> Get(Expression<Func<AssetDeploy, bool>> predicate = null);
        Task<ExcelPackage> DownloadAssetDeploy(DownloadAssetDeploy model);
    }
}