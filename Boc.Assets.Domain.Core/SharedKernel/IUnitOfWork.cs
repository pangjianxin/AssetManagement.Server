using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Core.SharedKernel
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveChangesAsync();
    }
}