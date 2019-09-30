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