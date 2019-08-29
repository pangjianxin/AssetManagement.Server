using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Boc.Assets.Domain.Models.Assets
{
    /// <summary>
    /// 机构管理的资产分类的注册表
    /// </summary>
    public class CategoryOrgRegistration : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private AssetCategory _assetCategory;
        private Organization _organization;
        public Guid AssetCategoryId { get; set; }
        public Guid OrganizationId { get; set; }
        public CategoryOrgRegistration(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public AssetCategory AssetCategory
        {
            get => _lazyLoader.Load(this, ref _assetCategory);
            set => _assetCategory = value;
        }

        public Organization Organization
        {
            get => _lazyLoader.Load(this, ref _organization);
            set => _organization = value;
        }
        public string Org2 { get; set; }
    }
}