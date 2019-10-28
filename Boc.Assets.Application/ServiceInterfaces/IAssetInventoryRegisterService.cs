using Boc.Assets.Application.Dto;
using Boc.Assets.Domain.Models.AssetInventories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetInventoryRegisterService : IApplicationService
    {
        IQueryable<AssetInventoryRegisterDto> Get(Expression<Func<AssetInventoryRegister, bool>> predicate = null);
    }
}