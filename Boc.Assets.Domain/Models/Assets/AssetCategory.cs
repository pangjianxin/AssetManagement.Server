using Boc.Assets.Domain.Core.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.Assets
{
    /// <summary>
    /// 该类在系统中的作用非常重要，因为每个资产分类属于某个管理机构进行管理。
    /// 当确定一个资产分类时，就确定了它所属的管理机构。
    /// </summary>
    public class AssetCategory : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<Asset> _assets;
        private ICollection<Maintainer> _maintainers;
        private ICollection<CategoryOrgRegistration> _categoryOrgRegistrations;
        public AssetCategory(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        public ICollection<Asset> Assets
        {
            get => _lazyLoader.Load(this, ref _assets);
            set => _assets = value;
        }

        public ICollection<Maintainer> Maintainers
        {
            get => _lazyLoader.Load(this, ref _maintainers);
            set => _maintainers = value;
        }
        public ICollection<CategoryOrgRegistration> CategoryOrgRegistrations
        {
            get => _lazyLoader.Load(this, ref _categoryOrgRegistrations);
            set => _categoryOrgRegistrations = value;
        }
        public string AssetFirstLevelCategory { get; set; }
        public string AssetSecondLevelCategory { get; set; }
        public string AssetThirdLevelCategory { get; set; }
        /// <summary>
        /// 资产计量单位
        /// </summary>
        public AssetMeteringUnit AssetMeteringUnit { get; set; }
        public override string ToString()
        {
            return $"{AssetFirstLevelCategory}|{AssetSecondLevelCategory}|{AssetThirdLevelCategory}";
        }
    }
    public enum AssetMeteringUnit
    {
        个 = 0,
        件 = 1,
        块 = 2,
        台 = 3,
        套 = 4,
        项 = 5
    }
}