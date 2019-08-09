﻿using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Models.Assets;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Repositories
{
    public interface IMaintainerRepository : IRepository<Maintainer>
    {
        Task<bool> AnyTargetMaintainer(string telephone);
        Task<bool> AnyAsync(Expression<Func<Maintainer, bool>> predicate);
    }
}