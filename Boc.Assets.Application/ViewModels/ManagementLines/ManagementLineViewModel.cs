using System;

namespace Boc.Assets.Application.ViewModels.ManagementLines
{
    public class ManagementLineViewModel : ViewModel
    {
        public Guid ManagementLineId { get; set; }
        public string ManagementLineName { get; set; }
        public string ManagementLineDescription { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}