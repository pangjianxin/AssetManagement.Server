using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using System;

namespace Boc.Assets.Domain.Models.Assets.Audit
{
    public class AssetExchange : AuditEntity
    {
        //for ef core
        public AssetExchange()
        {

        }
        public AssetExchange(IUser principal,
            Organization targetOrg,
            Organization exchangeOrg,
            Guid assetId,
            string assetName,
            string message) : base(principal, targetOrg, message)
        {
            ExchangeOrgId = exchangeOrg.Id;
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

        public override string ToString()
        {
            return "资产机构间调换申请";
        }
    }
}