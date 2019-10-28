﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Boc.Assets.Application.Dto
{
    public class AssetCategoryDto
    {
        [Key]
        public Guid Id { get; set; }
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
        public string AssetMeteringUnit { get; set; }
    }
}