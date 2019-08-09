using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Employee;
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