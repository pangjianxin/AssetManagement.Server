using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using System;

namespace Boc.Assets.Domain.Models.Assets.Audit
{
    public class AssetReturn : AuditEntity
    {
        //for ef core
        public AssetReturn()
        {

        }
        public AssetReturn(IUser principal,
            Organization targetOrg,
            Guid assetId,
            string assetName,
            string message) : base(principal, targetOrg, message)
        {
            AssetId = assetId;
            AssetName = assetName;
        }
        public Guid AssetId { get; set; }
        public string AssetName { get; set; }

        public override string ToString()
        {
            return "资产交回申请";
        }
    }
}