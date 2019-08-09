using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Events.Assets;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetExchangeService : IApplicationService
    {

        Task<PaginatedList<AssetExchangeDto>> PaginationAsync(SieveModel model,
            Expression<Func<AssetExchange, bool>> predicate);
        Task<AssetExchangeDto> RemoveAsync(Guid eventId);
        Task HandleAsync(HandleAssetExchange model);
        Task RevokeAsync(Revoke model);
        Task AssetExchangeAsync(ExchangeAsset model);
    }
}