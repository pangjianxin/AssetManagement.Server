using Boc.Assets.Domain.Models.Assets;
using System;

namespace Boc.Assets.Application.ViewModels.AssetCategory
{
    public class AssetCategoryViewModel : ViewModel
    {
        public Guid AssetCategoryId { get; set; }
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
    }
}