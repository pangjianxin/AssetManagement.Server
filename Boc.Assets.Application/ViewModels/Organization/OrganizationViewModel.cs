using System;

namespace Boc.Assets.Application.ViewModels.Organization
{
    public abstract class OrganizationViewModel : ViewModel
    {
        public Guid OrganizationId { get; set; }
        public string OrgIdentifier { get; set; }
        public string OrgNam { get; set; }
    }
}