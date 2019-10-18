using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Employee;
using Boc.Assets.Domain.Commands.Employee;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class EmployService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IUser _user;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployService(IMapper mapper,
            IBus bus,
            IUser user,
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _user = user;
            _employeeRepository = employeeRepository;
        }

        public async Task AddEmployee(AddEmployee model)
        {
            model.Org2 = _user.Org2;
            var command = _mapper.Map<AddEmployeeCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public IQueryable<EmployeeDto> Get(Expression<Func<Employee, bool>> predicate = null)
        {
            return _mapper.ProjectTo<EmployeeDto>(_employeeRepository.GetAll(predicate));
        }
    }
}