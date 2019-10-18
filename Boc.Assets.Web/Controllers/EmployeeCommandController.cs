using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Employee;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/employee")]
    public class EmployeeCommandController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeCommandController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IEmployeeService employeeService)
            : base(notifications, user)
        {
            _employeeService = employeeService;
        }

        [HttpPost("current/add")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> Post([FromBody]AddEmployee model)
        {
            await _employeeService.AddEmployee(model);
            return AppResponse(null, "操作成功");
        }
    }
}