using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Employee;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IEmployeeService employeeService)
            : base(notifications, user)
        {
            _employeeService = employeeService;
        }

        [HttpGet("current/employeesbyname")]
        [Permission(Permissions.Controllers.Employe, Permissions.Actions.Employe_Read)]
        public async Task<IActionResult> Get(string name)
        {
            var dtoList = await _employeeService.GetEmployeesByName(name);
            return AppResponse(dtoList, null);
        }

        [HttpPost("current/add")]
        [Permission(Permissions.Controllers.Employe, Permissions.Actions.Employe_Create)]
        public async Task<IActionResult> Add([FromBody]AddEmployee model)
        {
            await _employeeService.AddEmployee(model);
            return AppResponse(null, "操作成功");
        }

        [HttpGet("current/pagination")]
        [Permission(Permissions.Controllers.Employe, Permissions.Actions.Employe_Read)]
        public async Task<IActionResult> Pagination(SieveModel model)
        {
            var pagination = await _employeeService.Pagination(model);
            XPaginationHeader(pagination);
            return AppResponse(pagination, null);
        }
    }
}