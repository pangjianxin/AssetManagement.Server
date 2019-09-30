using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Boc.Assets.Domain.Models.Assets
{
    /// <summary>
    /// 机构管理的资产分类的登记表
    /// </summary>
    public class CategoryManageRegister : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;
        private AssetCategory _assetCategory;
        private Organization _manager;
        public Guid AssetCategoryId { get; set; }
        public Guid ManagerId { get; set; }
        public CategoryManageRegister(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        /// <summary>
        /// 该条管理注册表中对应的资产分类
        /// </summary>
        public AssetCategory AssetCategory
        {
            get => _lazyLoader.Load(this, ref _assetCategory);
            set => _assetCategory = value;
        }
        /// <summary>
        /// 对某个资产分类负责的机构
        /// </summary>
        public Organization Manager
        {
            get => _lazyLoader.Load(this, ref _manager);
            set => _manager = value;
        }
        public string Org2 { get; set; }
    }
}