using System;

namespace Boc.Assets.Domain.Core.SharedKernel
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {

    }
}