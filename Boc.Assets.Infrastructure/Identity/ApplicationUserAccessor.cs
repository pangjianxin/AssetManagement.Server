using Boc.Assets.Domain.Core.SharedKernel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Boc.Assets.Infrastructure.Identity
{
    public class ApplicationUserAccessor : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid OrgId => Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("orgId").Value);
        public string OrgIdentifier => _httpContextAccessor.HttpContext.User.FindFirst("orgIdentifier").Value;

        public string OrgNam => _httpContextAccessor.HttpContext.User.FindFirst("orgName").Value;

        public string Org2 => _httpContextAccessor.HttpContext.User.FindFirst("org2").Value;

        public Guid ManagementLineId => Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("managementLineId").Value);
        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }
    }
}