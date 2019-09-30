using Boc.Assets.Domain.ValueObjects;
using System;

namespace Boc.Assets.Domain.Models.Applies
{
    public class AssetApply : RequestEntity
    {
        //for ef core
        public AssetApply()
        {

        }
        public AssetApply(OrganizationInfo principal,
            OrganizationInfo targetOrg,
            string message,
            Guid assetCategoryId,
            string assetCategoryThirdLevel) : base(principal, targetOrg, message)
        {
            AssetCategoryId = assetCategoryId;
            AssetCategoryThirdLevel = assetCategoryThirdLevel;
        }
        public Guid AssetCategoryId { get; set; }
        public string AssetCategoryThirdLevel { get; set; }
    }
}