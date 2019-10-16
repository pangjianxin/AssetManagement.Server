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
    public interface IAssetExchangeService : IApplicationService
    {

        Task<PaginatedList<AssetExchangeDto>> PaginationAsync(SieveModel model,
            Expression<Func<AssetExchange, bool>> predicate);
        Task<bool> RemoveAssetExchangeAsync(RemoveAssetExchange model);
        Task HandleAssetExchangeAsync(HandleAssetExchange model);
        Task RevokeAssetExchangeAsync(RevokeAssetExchange model);
        Task CreateAssetExchangeAsync(ExchangeAsset model);
    }
}