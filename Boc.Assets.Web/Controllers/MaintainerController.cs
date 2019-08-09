using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Maintainers;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Web.Auth.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/maintainer")]
    public class MaintainerController : ApiController
    {
        private readonly IMaintainerService _maintainerService;

        public MaintainerController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMaintainerService maintainerService)
            : base(notifications, user)
        {
            _maintainerService = maintainerService;
        }

        [HttpPost("secondary/create")]
        [Permission(Permissions.Controllers.Maintainer, Permissions.Actions.Maintainer_Create_Secondary)]
        public async Task<IActionResult> AddMaintainer([FromBody] AddMaintainer model)
        {
            await _maintainerService.AddMaintainerAsync(model);
            return AppResponse(null, "操作成功");
        }

        [HttpGet("secondary/pagination")]
        [Permission(Permissions.Controllers.Maintainer, Permissions.Actions.Maintainer_Read_Secondary)]
        public async Task<IActionResult> SecondaryPaginationAsync(SieveModel model)
        {
            Expression<Func<Maintainer, bool>> predicate = it => it.OrganizationId == _user.OrgId;
            var pagination = await _maintainerService.PaginationAsync(model, predicate);
            XPaginationHeader(pagination);
            return AppResponse(pagination, null);
        }

        [HttpGet("current/pagination")]
        [Permission(Permissions.Controllers.Maintainer, Permissions.Actions.Maintainer_Read_Current)]
        public async Task<IActionResult> CurrentPagination(SieveModel model)
        {
            Expression<Func<Maintainer, bool>> predicate = it => it.Org2 == _user.Org2;
            var pagination = await _maintainerService.PaginationAsync(model, predicate);
            XPaginationHeader(pagination);
            return AppResponse(pagination);
        }
        [HttpDelete("secondary/delete")]
        [Permission(Permissions.Controllers.Maintainer, Permissions.Actions.Maintainer_Delete_Secondary)]
        public async Task<IActionResult> SeondaryDelete(DeleteMaintainer model)
        {
            await _maintainerService.DeleteAsync(model);
            return AppResponse(null, "操作成功");
        }

        [HttpGet("current/anymaintainer")]
        [Permission(Permissions.Controllers.Maintainer, Permissions.Actions.Maintainer_Read_Current)]
        public async Task<IActionResult> AnyMaintainerAsync(Guid assetCategoryId)
        {
            var any = await _maintainerService.AnyMaintainerAsync(assetCategoryId, _user.Org2);
            return AppResponse(any, null);
        }

        [HttpGet("current/maintainers")]
        [Permission(Permissions.Controllers.Maintainer, Permissions.Actions.Maintainer_Read_Current)]
        public async Task<IActionResult> MaintainersByCategoryId(Guid assetCategoryId)
        {
            var maintainerDtos = await _maintainerService.MaintainersByCategoryId(assetCategoryId, _user.Org2);
            return AppResponse(maintainerDtos);
        }
    }
}