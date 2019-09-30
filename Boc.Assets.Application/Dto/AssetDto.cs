using System;

namespace Boc.Assets.Application.Dto
{
    public class AssetDto
    {
        public Guid AssetId { get; set; }
        /// <summary>
        /// 资产名称
        /// </summary>
        public string AssetName { get; set; }
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
        /// <summary>
        /// 资产目前情况
        /// </summary>
        public string AssetStatus { get; set; }
        /// <summary>
        /// 最后一次修改备注
        /// </summary>
        public string LastDeployRecord { get; set; }
        /// <summary>
        /// 资产分类dto
        /// </summary>
        public AssetCategoryDto AssetCategoryDto { get; set; }
        /// <summary>
        /// 资产所属机构外键
        /// </summary>
        public Guid OrgInChargeId { get; set; }
        /// <summary>
        /// 资产存放机构名称
        /// </summary>
        public string OrgInUseName { get; set; }
        /// <summary>
        /// 资产存放机构号
        /// </summary>
        public string OrgInUseIdentifier { get; set; }
        /// <summary>
        /// 资产位置
        /// </summary>
        public string AssetLocation { get; set; }
    }

}