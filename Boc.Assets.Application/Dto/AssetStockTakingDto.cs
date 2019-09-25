using System;

namespace Boc.Assets.Application.Dto
{
    public class AssetStockTakingDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 盘点任务发布机构号
        /// </summary>
        public Guid PublisherId { get; set; }
        /// <summary>
        /// 盘点任务发布人名称
        /// </summary>
        public string PublisherName { get; set; }
        /// <summary>
        /// 盘点任务发布人的机构号
        /// </summary>
        public string PublisherIdentifier { get; set; }
        /// <summary>
        /// 盘点任务发布人的二级机构号
        /// </summary>
        public string PublisherOrg2 { get; set; }
        /// <summary>
        /// 盘点任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 盘点任务描述/备注
        /// </summary>
        public string TaskComment { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiryDateTime { get; set; }
        public string TimeProgress { get; set; }
    }
}