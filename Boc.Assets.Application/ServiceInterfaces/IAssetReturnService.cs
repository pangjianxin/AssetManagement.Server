using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Assets;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boc.Assets.Domain.Models.Applies;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetReturnService : IApplicationService
    {
        Task<PaginatedList<AssetReturnDto>> PaginationAsync(SieveModel model,
            Expression<Func<AssetReturn, bool>> predicate);
        Task<bool> RemoveAssetReturnAsync(RemoveAssetReturn model);
        Task RevokeAssetReturnAsync(RevokeAssetReturn model);
        Task HandleAssetReturnAsync(HandleAssetReturn model);
        Task CreateAssetReturnAsync(ReturnAsset model);
    }
}