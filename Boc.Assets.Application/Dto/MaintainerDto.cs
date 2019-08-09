using System;

namespace Boc.Assets.Application.Dto
{
    public class MaintainerDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 关联资产分类外键
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
        /// 服务商所属二级行
        /// </summary>
        public string Org2 { get; set; }
        /// <summary>
        /// 资产一级分类
        /// </summary>
        public string CategoryFirstLevel { get; set; }
        /// <summary>
        /// 资产二级分类
        /// </summary>
        public string CategorySecondLevel { get; set; }
        /// <summary>
        /// 资产三级分类
        /// </summary>
        public string CategoryThirdLevel { get; set; }
    }
}