using System;

namespace Boc.Assets.Application.ViewModels.OrganizationSpace
{
    public abstract class OrganizationSpaceViewModel : ViewModel
    {
        public Guid SpaceId { get; set; }
        public string SpaceName { get; set; }
        public string SpaceDescription { get; set; }
    }
}