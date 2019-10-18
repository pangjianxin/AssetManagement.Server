using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Models.Applies;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetExchangeService : IApplicationService
    {

        IQueryable<AssetExchangeDto> Get(Expression<Func<AssetExchange, bool>> predicate = null);
        Task<bool> RemoveAssetExchangeAsync(RemoveAssetExchange model);
        Task HandleAssetExchangeAsync(HandleAssetExchange model);
        Task RevokeAssetExchangeAsync(RevokeAssetExchange model);
        Task CreateAssetExchangeAsync(ExchangeAsset model);
    }
}