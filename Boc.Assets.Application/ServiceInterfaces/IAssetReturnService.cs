using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Models.Applies;
using Sieve.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

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