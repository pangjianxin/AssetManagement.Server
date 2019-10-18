using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.Employee;
using Boc.Assets.Domain.Models.Organizations;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IEmployeeService : IApplicationService
    {
        Task AddEmployee(AddEmployee model);
        IQueryable<EmployeeDto> Get(Expression<Func<Employee, bool>> predicate = null);
    }
}