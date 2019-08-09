using System;

namespace Boc.Assets.Application.ViewModels.Assets
{
    public abstract class AssetViewModel : ViewModel
    {
        /// <summary>
        /// 资产编号
        /// </summary>
        public Guid AssetId { get; set; }
        /// <summary>
        /// 资产分类编号
        /// </summary>
        public Guid AssetCategoryId { get; set; }
        /// <summary>
        /// 资产名称
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 资产说明,比如设备，可以用来描述设备的配置等信息
        /// </summary>
        public string AssetDescription { get; set; }
        /// <summary>
        /// 资产型号，如有
        /// </summary>
        public string AssetType { get; set; }
        /// <summary>
        /// 资产标签号
        /// </summary>
        public string AssetTagNumber { get; set; }
        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssetNo { get; set; }

        public string AssetLocation { get; set; }
        public Guid EventId { get; set; }
        public string Message { get; set; }
    }
}