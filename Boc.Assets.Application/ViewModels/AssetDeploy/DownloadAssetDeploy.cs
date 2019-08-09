using System;

namespace Boc.Assets.Application.ViewModels.AssetDeploy
{
    public class DownloadAssetDeploy : ViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? ExportOrgId { get; set; }
        public Guid? ImportOrgId { get; set; }
    }
}