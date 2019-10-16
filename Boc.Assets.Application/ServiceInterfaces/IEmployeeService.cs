using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ViewModels.Employee;
using Sieve.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IEmployeeService : IApplicationService
    {
        Task<List<EmployeeDto>> GetEmployeesByName(string name);
        Task AddEmployee(AddEmployee model);
        Task<PaginatedList<EmployeeDto>> Pagination(SieveModel model);
    }
}