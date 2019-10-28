using Boc.Assets.Domain.Core.Models;
using System;

namespace Boc.Assets.Domain.Models.Assets
{
    public class Maintainer : EntityBase
    {
        public Maintainer()
        {
        }
        /// <summary>
        /// 资产分类外键
        /// </summary>
        public Guid AssetCategoryId { get; set; }
        /// <summary>
        /// 服务商信息发布机构
        /// </summary>
        public Guid OrganizationId { get; set; }
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
        /// <summary>
        /// 服务商归属二级行
        /// </summary>
        public string Org2 { get; set; }
        /// <summary>
        /// 维修信息的关联资产分类
        /// </summary>
        public virtual AssetCategory AssetCategory { get; set; }
    }
}