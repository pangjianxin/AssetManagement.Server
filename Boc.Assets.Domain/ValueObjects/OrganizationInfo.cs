using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.ValueObjects
{
    public class OrganizationInfo : ValueObject
    {
        //for ef core construct
        public OrganizationInfo()
        {
            
        }
        public OrganizationInfo(Guid orgId, string orgIdentifier, string orgName)
        {
            OrgId = orgId;
            OrgIdentifier = orgIdentifier;
            OrgNam = orgName;
        }
        public Guid OrgId { get; set; }
        public string OrgIdentifier { get; set; }
        public string OrgNam { get; set; }
    }
}