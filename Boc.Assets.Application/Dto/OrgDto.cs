using System;

namespace Boc.Assets.Application.Dto
{
    public class OrgDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string OrgIdentifier { get; set; }
        public string OrgNam { get; set; }
        public string OrgShortNam { get; set; }
        public string UpOrg { get; set; }
        public string OrgLvl { get; set; }
        public string Org1 { get; set; }
        public string OrgNam1 { get; set; }
        public string Org2 { get; set; }
        public string OrgNam2 { get; set; }
        public string Org3 { get; set; }
        public string OrgNam3 { get; set; }
        public int Role { get; set; }
        public string RoleDescription { get; set; }
    }
}