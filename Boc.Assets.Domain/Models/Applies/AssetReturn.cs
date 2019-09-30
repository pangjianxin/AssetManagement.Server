using Boc.Assets.Domain.ValueObjects;
using System;

namespace Boc.Assets.Domain.Models.Applies
{
    public class AssetReturn : RequestEntity
    {
        //for ef core
        public AssetReturn()
        {

        }
        public AssetReturn(OrganizationInfo principal,
            OrganizationInfo targetOrg,
            string message,
            Guid assetId,
            string assetName) : base(principal, targetOrg, message)
        {
            AssetId = assetId;
            AssetName = assetName;
        }
        public Guid AssetId { get; set; }
        public string AssetName { get; set; }
    }
}