using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Login;
using Boc.Assets.Application.ViewModels.Organization;
using Boc.Assets.Domain.Authentication;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boc.Assets.Web.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IJwtFactory _jwtFactory;

        public AuthController(
            INotificationHandler<DomainNotification> notifications,
            IUser user,
            IOrganizationService organizationService,
            IJwtFactory jwtFactory)
            : base(notifications, user)
        {
            _organizationService = organizationService;
            _jwtFactory = jwtFactory;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var accessToken = await _organizationService.LoginAsync(model);
            return AppResponse(new
            {
                access_token = accessToken
            }, "登录成功");
        }
        /// <summary>
        /// 修改用户简称
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("changeOrgShortName")]
        [Authorize]
        public async Task<IActionResult> ChangeOrgShortName([FromBody]ChangeOrgShortName model)
        {
            var token = await _organizationService.ChangeOrgShortNameAsync(model);
            return AppResponse(new { access_token = token }, "操作成功");
        }
        /// <summary>
        /// 获取账户
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("accounts")]
        [Authorize]
        public async Task<IActionResult> Accounts([FromQuery]SieveModel model)
        {
            var result = await _organizationService.PaginationAsync(model);
            XPaginationHeader(result);
            return AppResponse(result);
        }
        /// <summary>
        /// 获取前二十个搜索结果
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [HttpGet("accounts/search")]
        [Authorize]
        public async Task<IActionResult> GetTwentyAsync(string searchInput)
        {
            var result = await _organizationService.GetTwentyAsync(searchInput, _user.Org2);
            return AppResponse(result);
        }
        /// <summary>
        /// 重置密码
        /// 二级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("resetPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] ResetOrgPassword model)
        {

            await _organizationService.ResetOrgPassword(model);
            return AppResponse(null, "操作成功");
        }
        /// <summary>
        /// 修改密码
        /// 当前机构权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("changeOrgPassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeOrgPassword model)
        {
            var token = await _organizationService.ChangeOrgPassword(model);
            return AppResponse(new { access_token = token }, "修改密码成功");
        }
        /// <summary>
        /// 获取某个二级行下面的所有机构
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("accounts/org2")]
        [Authorize]
        public async Task<IActionResult> AccountsByOrg2(SieveModel model)
        {
            var result = await _organizationService.PaginationAsync(model, it => it.Org2 == _user.Org2);
            XPaginationHeader(result);
            return AppResponse(result, "成功");
        }
        [HttpGet("pushHash")]
        [AllowAnonymous]
        public async Task<IActionResult> PushHash()
        {
            await _organizationService.PushHash();
            return AppResponse(null, "操作成功");
        }
    }
}
