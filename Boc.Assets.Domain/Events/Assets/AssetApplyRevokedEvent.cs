﻿using Boc.Assets.Domain.Core.Events;
using Boc.Assets.Domain.Models.Assets.Audit;

namespace Boc.Assets.Domain.Events.Assets
{
    public class AssetApplyRevokedEvent : Event
    {
        public AssetApply AssetApply { get; }
        public string Message { get; }
        public AssetApplyRevokedEvent(AssetApply assetApply, string message)
        {
            AssetApply = assetApply;
            Message = message;
        }
        public override string ToString()
        {
            return "资产申请已撤销";
        }
    }
}