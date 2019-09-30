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
        private ICollection<CategoryManageRegister> _categoryManageRegisters;
        public AssetCategory(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        /// <summary>
        /// 该条类别下的所有资产
        /// </summary>
        public ICollection<Asset> Assets
        {
            get => _lazyLoader.Load(this, ref _assets);
            set => _assets = value;
        }
        /// <summary>
        /// 该条类别下的所有维修信息
        /// </summary>
        public ICollection<Maintainer> Maintainers
        {
            get => _lazyLoader.Load(this, ref _maintainers);
            set => _maintainers = value;
        }
        /// <summary>
        /// 该条资产类别下的所有登记的管理机构
        /// </summary>
        public ICollection<CategoryManageRegister> CategoryManageRegisters
        {
            get => _lazyLoader.Load(this, ref _categoryManageRegisters);
            set => _categoryManageRegisters = value;
        }
        /// <summary>
        /// 资产大类
        /// </summary>
        public string AssetFirstLevelCategory { get; set; }
        /// <summary>
        /// 资产中类
        /// </summary>
        public string AssetSecondLevelCategory { get; set; }
        /// <summary>
        /// 资产小类
        /// </summary>
        public string AssetThirdLevelCategory { get; set; }
        /// <summary>
        /// 资产计量单位
        /// </summary>
        public AssetMeteringUnit AssetMeteringUnit { get; set; }
        public override string ToString()
        {
            return $"{AssetFirstLevelCategory}|{AssetSecondLevelCategory}|{AssetThirdLevelCategory}";
        }
        /// <summary>
        /// 修改该条资产类别的计量单位
        /// </summary>
        /// <param name="unit"></param>
        public void ChangeUnit(AssetMeteringUnit unit)
        {
            AssetMeteringUnit = unit;
        }
    }
    public enum AssetMeteringUnit
    {
        个 = 1,
        件 = 2,
        块 = 3,
        台 = 4,
        套 = 5,
        项 = 6
    }
}