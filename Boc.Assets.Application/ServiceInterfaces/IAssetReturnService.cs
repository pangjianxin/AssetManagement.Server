using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Models.Assets.Audit;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetReturnService : IApplicationService
    {
        Task<PaginatedList<AssetReturnDto>> PaginationAsync(SieveModel model,
            Expression<Func<AssetReturn, bool>> predicate);
        Task<AssetReturnDto> RemoveAsync(Guid eventId);
        Task RevokeAsync(Revoke model);
        Task HandleAsync(HandleAssetReturn model);
        Task AssetReturnAsync(ReturnAsset model);
    }
}