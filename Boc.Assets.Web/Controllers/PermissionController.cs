using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Permission;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("/api/permission")]
    public class PermissionController : ApiController
    {
        private readonly IMemoryCache _cache;
        private readonly IPermissionService _permissionService;
        private readonly IOrganizationRoleService _orgRoleService;
        public PermissionController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMemoryCache cache,
            IPermissionService permissionService,
            IOrganizationRoleService orgRoleService) : base(notifications, user)
        {
            _cache = cache;
            _permissionService = permissionService;
            _orgRoleService = orgRoleService;
        }
        [HttpGet("all")]
        [Permission(Permissions.Controllers.Permission, Permissions.Actions.Permission_Read_Secondary)]
        public IActionResult GetPermissions()
        {
            return AppResponse(ResourceData.Resources, null);
        }
        [HttpPut("modifypermissions")]
        [Permission(Permissions.Controllers.Permission, Permissions.Actions.Permission_Modify_Secondary)]
        public async Task<IActionResult> ModifyPermissionByRoleId([FromBody]ModifyPermission model)
        {

            await _permissionService.ModifyRolePermission(model);
            return AppResponse(null, "操作成功");
        }
        [HttpGet("roles")]
        [Authorize]
        public async Task<IActionResult> GetAllRols([FromQuery]int role)
        {
            var roles = await _orgRoleService.GetAll(role);
            return AppResponse(roles);
        }
    }
}