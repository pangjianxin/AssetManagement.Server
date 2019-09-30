using Boc.Assets.Domain.ValueObjects;
using System;

namespace Boc.Assets.Domain.Models.Applies
{
    public class AssetExchange : RequestEntity
    {
        //for ef core
        public AssetExchange()
        {

        }
        public AssetExchange(OrganizationInfo principal,
            OrganizationInfo targetOrg,
            OrganizationInfo exchangeOrg,
            string message,
            Guid assetId,
            string assetName) : base(principal, targetOrg, message)
        {
            ExchangeOrgId = exchangeOrg.OrgId;
            ExchangeOrgIdentifier = exchangeOrg.OrgIdentifier;
            ExchangeOrgNam = exchangeOrg.OrgNam;
            AssetId = assetId;
            AssetName = assetName;
        }
        public Guid AssetId { get; set; }
        public string AssetName { get; set; }
        public Guid ExchangeOrgId { get; set; }
        public string ExchangeOrgIdentifier { get; set; }
        public string ExchangeOrgNam { get; set; }
    }
}