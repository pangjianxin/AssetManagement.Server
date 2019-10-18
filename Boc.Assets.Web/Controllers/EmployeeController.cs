using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Web.Controllers
{
    public class EmployeeController : ODataController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUser _user;

        public EmployeeController(
            IUser user,
            IEmployeeService employeeService)

        {
            _employeeService = employeeService;
            _user = user;
        }

        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<EmployeeDto> Get(string search)
        {
            Expression<Func<Employee, bool>> predicate = it =>
                it.Identifier.Contains(search) || it.Name.Contains(search);
            return _employeeService.Get(predicate);
        }
        [EnableQuery]
        [Authorize(Policy = "user")]
        public IQueryable<EmployeeDto> Get()
        {
            Expression<Func<Employee, bool>> predicate = it => it.Org2 == _user.Org2;
            return _employeeService.Get(predicate);

        }
    }
}