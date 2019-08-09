using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Domain.Events;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface INonAuditEventService : IApplicationService
    {
        Task<PaginatedList<NonAuditEventDto>> PaginationAsync(SieveModel model,
            Expression<Func<NonAuditEvent, bool>> predicate);
        Task<NonAuditEventDto> RemoveAsync(Guid eventId);

    }
}