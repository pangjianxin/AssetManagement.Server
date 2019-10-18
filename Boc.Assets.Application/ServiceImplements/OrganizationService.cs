using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Login;
using Boc.Assets.Application.ViewModels.Organization;
using Boc.Assets.Domain.Commands.Organization;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationService(
            IMapper mapper,
            IOrganizationRepository orgRepository,
            IBus bus,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _orgRepository = orgRepository;
            _bus = bus;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> ChangeOrgShortNameAsync(ChangeOrgShortName model)
        {
            var command = _mapper.Map<ChangeOrgShortNameCommand>(model);
            return await _bus.SendCommandAsync(command);
        }
        public async Task<string> ChangeOrgPassword(ChangeOrgPassword model)
        {
            var command = _mapper.Map<ChangeOrgPasswordCommand>(model);
            return await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 用户重置密码用例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> ResetOrgPassword(ResetOrgPassword model)
        {
            var command = _mapper.Map<ResetOrgPasswordCommand>(model);
            return await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 用户登录用例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> LoginAsync(Login model)
        {
            var command = _mapper.Map<LoginCommand>(model);
            return await _bus.SendCommandAsync(command);
        }
        public async Task<OrgDto> Get(Guid id)
        {
            var org = await _orgRepository.GetByIdAsync(id);
            return _mapper.Map<OrgDto>(org);
        }
        public async Task<IEnumerable<OrgDto>> Get(string searchInput, string org2)
        {
            var source = _orgRepository.GetAll(it => it.Org2 == org2 && it.OrgNam.Contains(searchInput))
                .OrderByDescending(it => it.Id).Take(20)
                .ProjectTo<OrgDto>(_mapper.ConfigurationProvider);
            var result = await source.ToListAsync();
            return result;
        }

        public async Task PushHash()
        {
            var orgs = await _orgRepository.GetAllListAsync();
            foreach (var item in orgs)
            {
                var salt = Guid.NewGuid().ToByteArray();
                var hash = _passwordHasher.Hash("000000", salt);
                item.Hash = hash;
                item.Salt = salt;
                _orgRepository.Update(item);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public IQueryable<OrgDto> Get(Expression<Func<Organization, bool>> predicate = null)
        {
            return _mapper.ProjectTo<OrgDto>(_orgRepository.GetAll(predicate));
        }
    }
}