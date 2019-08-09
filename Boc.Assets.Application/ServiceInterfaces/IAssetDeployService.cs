using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.AssetDeploy;
using Boc.Assets.Domain.Models.Assets;
using OfficeOpenXml;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetDeployService : IApplicationService
    {

        Task<PaginatedList<AssetDeployDto>> PaginationAsync(SieveModel model, Expression<Func<AssetDeploy, bool>> predicate);
        Task<ExcelPackage> DownloadAssetDeploy(DownloadAssetDeploy model);
    }
}