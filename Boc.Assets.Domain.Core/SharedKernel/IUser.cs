using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Boc.Assets.Domain.Core.SharedKernel
{
    public interface IUser
    {
        Guid OrgId { get; }
        string OrgIdentifier { get; }
        string OrgNam { get; }
        string Org2 { get; }
        Guid ManagementLineId { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}