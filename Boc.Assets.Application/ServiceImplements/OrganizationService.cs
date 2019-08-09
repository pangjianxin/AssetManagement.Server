using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.Sieve.Services;
using Boc.Assets.Application.ViewModels.Login;
using Boc.Assets.Application.ViewModels.Organization;
using Boc.Assets.Domain.Commands.Organization;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationRepository _orgRepository;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public OrganizationService(
            IMapper mapper,
            IOrganizationRepository orgRepository,
            IBus bus,
            ISieveProcessor sievingProcessor,
            IOptions<SieveOptions> sieveOptions)
        {
            _mapper = mapper;
            _orgRepository = orgRepository;
            _bus = bus;
            _sieveProcessor = sievingProcessor;
            _sieveOptions = sieveOptions.Value;
        }
        public async Task ChangeOrgShortNameAsync(ChangeOrgShortName model)
        {
            var command = _mapper.Map<ChangeOrgShortNameCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task<OrgDto> GetByOrgIdentifierAsync(string orgIdentifier)
        {
            var orgDto = _mapper.Map<OrgDto>(await _orgRepository.GetByOrgIdentifierAsync(orgIdentifier));
            return orgDto;
        }

        public async Task<bool> CheckLoginCredentialAsync(Login model)
        {
            var org = await _orgRepository.GetByOrgIdentifierAsync(model.OrgIdentifier);
            if (org == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification("登录", "用户名不存在"));
                return false;
            }
            if (org.Password != model.Password)
            {
                await _bus.RaiseEventAsync(new DomainNotification("登录", "密码不正确"));
                return false;
            }
            return true;
        }
        public async Task<OrgDto> GetByIdAsync(Guid id)
        {
            var org = await _orgRepository.GetByIdAsync(id);
            return _mapper.Map<OrgDto>(org);
        }
        public async Task<PaginatedList<OrgDto>> PaginationAsync(SieveModel model, Expression<Func<Organization, bool>> predicate = null)
        {
            var entities = _orgRepository.GetAll(predicate);
            entities = _sieveProcessor.Apply(model, entities, applyPagination: false);
            var count = entities.Count();
            var result = _sieveProcessor.Apply(model, entities).ProjectTo<OrgDto>(_mapper.ConfigurationProvider);
            var data = await result.ToListAsync();
            return new PaginatedList<OrgDto>(_sieveOptions, model.Page, model.PageSize, count, data);
        }
        public async Task ResetOrgPassword(ResetOrgPassword model)
        {
            var command = _mapper.Map<ResetOrgPasswordCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task ChangeOrgPassword(ChangeOrgPassword model)
        {
            var command = _mapper.Map<ChangeOrgPasswordCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task<IEnumerable<OrgDto>> GetTwentyAsync(string searchInput, string org2)
        {
            var source = _orgRepository.GetAll(it => it.Org2 == org2 && it.OrgNam.Contains(searchInput))
                .OrderByDescending(it => it.Id).Take(20)
                .ProjectTo<OrgDto>(_mapper.ConfigurationProvider);
            var result = await source.ToListAsync();
            return result;
        }
    }
}