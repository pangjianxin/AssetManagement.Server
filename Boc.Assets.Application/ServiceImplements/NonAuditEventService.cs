using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boc.Assets.Application.Sieve.Services;

namespace Boc.Assets.Application.Pagination
{
    public class NonAuditEventService : INonAuditEventService
    {
        private readonly IEventSource<NonAuditEvent> _nonAuditEventSource;
        private readonly IUser _user;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;
        private readonly SieveOptions _sieveOptions;

        public NonAuditEventService(IEventSource<NonAuditEvent> nonAuditEventSource,
            IUser user,
            ISieveProcessor sieveProcessor,
            IOptions<SieveOptions> options,
            IMapper mapper)
        {
            _nonAuditEventSource = nonAuditEventSource;
            _user = user;
            _sieveProcessor = sieveProcessor;
            _mapper = mapper;
            _sieveOptions = options.Value;
        }

        public async Task<PaginatedList<NonAuditEventDto>> PaginationAsync(SieveModel model, Expression<Func<NonAuditEvent, bool>> predicate)
        {
            var events = _nonAuditEventSource.GetAll(predicate);
            var count = await _sieveProcessor.Apply(model, events, applyPagination: false).CountAsync();
            var result = _sieveProcessor.Apply(model, events).ProjectTo<NonAuditEventDto>(_mapper.ConfigurationProvider);
            var pagination = await result.ToListAsync();
            return new PaginatedList<NonAuditEventDto>(_sieveOptions, model.Page, model.PageSize, count, pagination);
        }

        public async Task<NonAuditEventDto> RemoveAsync(Guid eventId)
        {
            var @event = await _nonAuditEventSource.GetByIdAsync(eventId);
            @event = await _nonAuditEventSource.RemoveAsync(@event);
            return _mapper.Map<NonAuditEventDto>(@event);
        }
    }
}