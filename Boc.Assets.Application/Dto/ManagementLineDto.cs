using System;

namespace Boc.Assets.Application.Dto
{
    public class ManagementLineDto
    {
        public Guid ManagementLineId { get; set; }
        public string ManagementLineName { get; set; }
        public string ManagementLineDescription { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}