﻿using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;
using System;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetApplyCreatedEvent : Event
    {
        public string Message { get; }
        public AssetApplyCreatedEvent(Guid aggregateId, AssetApply assetApply, string message)
        {
            AssetCategory = assetApply.AssetCategoryThirdLevel;
            RequestOrgIdentifier = assetApply.RequestOrgIdentifier;
            TargetOrgIdentifier = assetApply.TargetOrgIdentifier;
            Message = message;
            AggregateId = aggregateId;
        }
        public string AssetCategory { get; set; }
        public string RequestOrgIdentifier { get; set; }
        public string TargetOrgIdentifier { get; set; }
    }
}