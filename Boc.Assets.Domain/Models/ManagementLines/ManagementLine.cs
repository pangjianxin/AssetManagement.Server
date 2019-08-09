using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boc.Assets.Domain.Models.ManagementLines
{
    public class ManagementLine : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;

        public ManagementLine(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        private ICollection<Organization> _organizations;
        private ICollection<AssetCategory> _assetCategories;
        /// <summary>
        /// 该条线所管理的机构
        /// </summary>
        public ICollection<Organization> Organizations
        {
            get => _lazyLoader.Load(this, ref _organizations);
            set => _organizations = value;
        }
        /// <summary>
        /// 该条线所管理的资产分类
        /// </summary>
        public ICollection<AssetCategory> AssetCategories
        {
            get => _lazyLoader.Load(this, ref _assetCategories);
            set => _assetCategories = value;
        }
        public string ManagementLineName { get; set; }
        public string ManagementLineDescription { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        #region methods
        public IEnumerable<Organization> FindTargetOrganizations(string org2)
        {
            return Organizations.Where(it => it.Org2 == org2);
        }
        #endregion
    }
}