using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.AssetStockTakings;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.Organizations
{
    public class Organization : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<OrganizationSpace> _organizationSpaces;
        private ICollection<Asset> _assetsBelongs;
        private ICollection<Asset> _assetsStores;
        private ICollection<AssetStockTakingOrganization> _assetStockTakingOrganizations;
        private OrganizationRole _role;
        public Organization(ILazyLoader lazyLoader = null)
        {
            _lazyLoader = lazyLoader;
        }
        public Guid? ManagementLineId { get; set; }
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
        public OrganizationStatus Status { get; set; } = OrganizationStatus.正常;
        public string Password { get; set; }
        /// <summary>
        /// 当前机构的角色
        /// </summary>
        public OrganizationRole Role
        {
            get => _lazyLoader.Load(this, ref _role);
            set => _role = value;
        }
        /// <summary>
        /// 机构的空间
        /// </summary>
        public ICollection<OrganizationSpace> OrganizationSpaces
        {
            get => _lazyLoader.Load(this, ref _organizationSpaces);
            set => _organizationSpaces = value;
        }
        /// <summary>
        /// 机构名下存放的资产
        /// </summary>
        public ICollection<Asset> AssetsBelongs
        {
            get => _lazyLoader.Load(this, ref _assetsBelongs);
            set => _assetsBelongs = value;
        }
        /// <summary>
        /// 机构负责的资产
        /// </summary>
        public ICollection<Asset> AssetsStores
        {
            get => _lazyLoader.Load(this, ref _assetsStores);
            set => _assetsStores = value;
        }
        /// <summary>
        /// 固定资产盘点参与机构
        /// </summary>
        public ICollection<AssetStockTakingOrganization> AssetStockTakingOrganizations
        {
            get => _lazyLoader.Load(this, ref _assetStockTakingOrganizations);
            set => _assetStockTakingOrganizations = value;
        }
        /// <summary>
        /// 机构发布的维修信息
        /// </summary>
        #region methods

        public void ChangeOrgShortName(string orgShortName)
        {
            this.OrgShortNam = orgShortName;
        }

        public void ResetPassword()
        {
            Password = "000000";
        }

        public void ChangeOrgPassword(string newPassword)
        {
            Password = newPassword;
        }

        public void OrganizationRevocation()
        {
            Status = OrganizationStatus.撤销;
        }
        #endregion

    }
    public enum OrganizationStatus
    {
        正常, 撤销
    }
}