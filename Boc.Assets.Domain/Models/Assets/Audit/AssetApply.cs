using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using System;

namespace Boc.Assets.Domain.Models.Assets.Audit
{
    public class AssetApply : AuditEntity
    {
        //for ef core
        public AssetApply()
        {

        }
        public AssetApply(IUser principal,
            Organization targetOrg,
            Guid assetCategoryId,
            string assetCategoryThirdLevel,
            string message) : base(principal, targetOrg, message)
        {
            AssetCategoryId = assetCategoryId;
            AssetCategoryThirdLevel = assetCategoryThirdLevel;
        }
        public Guid AssetCategoryId { get; set; }
        public string AssetCategoryThirdLevel { get; set; }

        public override string ToString()
        {
            return "资产申请事件";
        }
    }
}