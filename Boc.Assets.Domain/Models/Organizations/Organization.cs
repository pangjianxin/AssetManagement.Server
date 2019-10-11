using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.Organizations
{
    public class Organization : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<OrganizationSpace> _organizationSpaces;
        private ICollection<Asset> _assetsInCharge;
        private ICollection<Asset> _assetsInUse;
        private ICollection<AssetInventoryRegister> _assetInventoryRegisters;
        private ICollection<CategoryManageRegister> _categoryManageRegisters;
        private OrganizationRole _role;
        public Organization(ILazyLoader lazyLoader = null)
        {
            _lazyLoader = lazyLoader;
        }
        /// <summary>
        /// 角色(外键)
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 机构号
        /// </summary>
        public string OrgIdentifier { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgNam { get; set; }
        /// <summary>
        /// 机构简称
        /// </summary>
        public string OrgShortNam { get; set; }
        /// <summary>
        /// 上级行机构号
        /// </summary>
        public string UpOrg { get; set; }
        /// <summary>
        /// 机构层级
        /// </summary>
        public string OrgLvl { get; set; }
        /// <summary>
        /// 一级行机构号
        /// </summary>
        public string Org1 { get; set; }
        /// <summary>
        /// 一级行名称
        /// </summary>
        public string OrgNam1 { get; set; }
        /// <summary>
        /// 二级行机构号
        /// </summary>
        public string Org2 { get; set; }
        /// <summary>
        /// 二级行名称
        /// </summary>
        public string OrgNam2 { get; set; }
        /// <summary>
        /// 三几行机构号
        /// </summary>
        public string Org3 { get; set; }
        /// <summary>
        /// 三级行名称
        /// </summary>
        public string OrgNam3 { get; set; }
        /// <summary>
        /// 机构当前状态
        /// </summary>
        public OrganizationStatus Status { get; set; } = OrganizationStatus.正常;
        /// <summary>
        /// 密码hash
        /// </summary>
        public byte[] Hash { get; set; }
        /// <summary>
        /// 密码相关的盐
        /// </summary>

        public byte[] Salt { get; set; }
        /// <summary>
        /// 当前机构的角色
        /// </summary>
        public OrganizationRole Role
        {
            get => _lazyLoader.Load(this, ref _role);
            set => _role = value;
        }
        /// <summary>
        /// 机构的空间(一对多)
        /// </summary>
        public ICollection<OrganizationSpace> OrganizationSpaces
        {
            get => _lazyLoader.Load(this, ref _organizationSpaces);
            set => _organizationSpaces = value;
        }
        /// <summary>
        /// 机构名下存放的资产(一对多)
        /// </summary>
        public ICollection<Asset> AssetsInCharge
        {
            get => _lazyLoader.Load(this, ref _assetsInCharge);
            set => _assetsInCharge = value;
        }
        /// <summary>
        /// 机构负责的资产(一对多)
        /// </summary>
        public ICollection<Asset> AssetsInUse
        {
            get => _lazyLoader.Load(this, ref _assetsInUse);
            set => _assetsInUse = value;
        }
        /// <summary>
        /// 固定资产盘点参与机构(一对多)
        /// </summary>
        public ICollection<AssetInventoryRegister> AssetInventoryRegisters
        {
            get => _lazyLoader.Load(this, ref _assetInventoryRegisters);
            set => _assetInventoryRegisters = value;
        }
        /// <summary>
        /// 机构管理的资产分类的注册表(一对多)
        /// </summary>
        public ICollection<CategoryManageRegister> CategoryManageRegisters
        {
            get => _lazyLoader.Load(this, ref _categoryManageRegisters);
            set => _categoryManageRegisters = value;
        }
        #region methods
        /// <summary>
        /// 获取机构的值对象
        /// </summary>
        /// <returns></returns>
        public OrganizationInfo GetValueObject()
        {
            return new OrganizationInfo(Id, OrgIdentifier, OrgNam);
        }
        public string ChangeOrgShortName(string orgShortName)
        {
            OrgShortNam = orgShortName;
            return OrgShortNam;
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