using System;

namespace Boc.Assets.Application.ViewModels.Maintainers
{
    public abstract class MaintainerViewModel : ViewModel
    {
        /// <summary>
        /// 服务商主键
        /// </summary>
        public Guid MaintainerId { get; set; }
        /// <summary>
        /// 资产分类ID
        /// </summary>
        public Guid AssetCategoryId { get; set; }
        /// <summary>
        ///维修公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 维修工人名字
        /// </summary>
        public string MaintainerName { get; set; }
        /// <summary>
        /// 维修工人手机号
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 维修工人座机
        /// </summary>
        public string OfficePhone { get; set; }
    }
}