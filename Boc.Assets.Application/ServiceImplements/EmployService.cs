using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Employee;
using Boc.Assets.Domain.Commands.Employee;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class EmployService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IUser _user;
        private readonly SieveOptions _sieveOptions;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployService(IMapper mapper,
            IBus bus,
            ISieveProcessor sieveProcessor,
            IUser user,
            IOptions<SieveOptions> sieveOptions,
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _sieveProcessor = sieveProcessor;
            _user = user;
            _sieveOptions = sieveOptions.Value;
            _employeeRepository = employeeRepository;
        }

        public async Task AddEmployee(AddEmployee model)
        {
            model.Org2 = _user.Org2;
            var command = _mapper.Map<AddEmployeeCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task<PaginatedList<EmployeeDto>> Pagination(SieveModel model)
        {
            var entities = _employeeRepository.GetAll();
            var count = await _sieveProcessor.Apply(model, entities, applyPagination: false).CountAsync();
            var result = _sieveProcessor.Apply(model, entities).ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider);
            var pagination = await result.ToListAsync();
            return new PaginatedList<EmployeeDto>(_sieveOptions, model.Page, model.PageSize, count, pagination);
        }

        public async Task<List<EmployeeDto>> GetEmployeesByName(string name)
        {
            Expression<Func<Employee, bool>> predicate = it => it.Org2 == _user.Org2 && it.Name.Contains(name);
            return await _employeeRepository.GetAll(predicate).ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}