using Boc.Assets.Domain.Core.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Boc.Assets.Domain.Models.Organizations
{
    public class OrganizationSpace : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private Organization _organization;
        public OrganizationSpace() { }
        public OrganizationSpace(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public Guid OrgId { get; set; }
        public string OrgIdentifier { get; set; }
        public string OrgName { get; set; }
        public Organization Organization
        {
            get => _lazyLoader.Load(this, ref _organization);
            set => _organization = value;
        }
        public string SpaceName { get; set; }
        public string SpaceDescription { get; set; }
        #region methods

        public void ModifySpaceInfo(string spaceName, string spaceDescription)
        {
            SpaceName = spaceName;
            SpaceDescription = spaceDescription;
        }
        #endregion  
    }
}