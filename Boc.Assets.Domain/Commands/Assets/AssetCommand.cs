using Boc.Assets.Domain.Core.Commands;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public abstract class AssetCommand : Command
    {
        /// <summary>
        /// 资产序号
        /// </summary>
        public Guid AssetId { get; protected set; }
        /// <summary>
        /// 资产分类序号
        /// </summary>
        public Guid AssetCategoryId { get; protected set; }
        /// <summary>
        /// 资产名称
        /// </summary>
        public string AssetName { get; protected set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; protected set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; protected set; }
        /// <summary>
        /// 资产说明,比如设备，可以用来描述设备的配置等信息
        /// </summary>
        public string AssetDescription { get; protected set; }
        /// <summary>
        /// 资产型号，如有
        /// </summary>
        public string AssetType { get; protected set; }
        /// <summary>
        /// 资产标签号
        /// </summary>
        public string AssetTagNumber { get; protected set; }
        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssetNo { get; protected set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? CreateDateTime { get; protected set; }
        /// <summary>
        /// 存放位置
        /// </summary>
        public string AssetLocation { get; protected set; }
    }
}