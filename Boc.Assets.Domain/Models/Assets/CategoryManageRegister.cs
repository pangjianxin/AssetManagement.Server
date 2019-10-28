using Boc.Assets.Domain.Core.Models;
using Boc.Assets.Domain.Models.Organizations;
using System;

namespace Boc.Assets.Domain.Models.Assets
{
    /// <summary>
    /// 机构管理的资产分类的登记表
    /// </summary>
    public class CategoryManageRegister : EntityBase
    {
        public CategoryManageRegister()
        {
        }
        public Guid AssetCategoryId { get; set; }
        public Guid ManagerId { get; set; }
        public string Org2 { get; set; }
        /// <summary>
        /// 该条管理注册表中对应的资产分类
        /// </summary>
        public virtual AssetCategory AssetCategory { get; set; }
        /// <summary>
        /// 对某个资产分类负责的机构
        /// </summary>
        public virtual Organization Manager { get; set; }
    }
}