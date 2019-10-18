﻿using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Models.Applies;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetApplyService : IApplicationService
    {
        IQueryable<AssetApplyDto> Get(Expression<Func<AssetApply, bool>> predicate=null);
        Task RemoveAsync(RemoveAssetApply model);
        Task ApplyAssetAsync(ApplyAsset model);
        Task RevokeAsync(RevokeAssetApply model);
        Task HandleAsync(HandleAssetApply model);
    }
}