using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Models.Applies;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetReturnService : IApplicationService
    {
        IQueryable<AssetReturnDto> Get(Expression<Func<AssetReturn, bool>> predicat = null);
        Task<bool> RemoveAssetReturnAsync(RemoveAssetReturn model);
        Task RevokeAssetReturnAsync(RevokeAssetReturn model);
        Task HandleAssetReturnAsync(HandleAssetReturn model);
        Task CreateAssetReturnAsync(ReturnAsset model);
    }
}