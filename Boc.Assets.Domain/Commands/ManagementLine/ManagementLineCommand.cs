using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.ManagementLine
{
    public abstract class ManagementLineCommand : Command
    {
        public ManagementLineCommand(IUser principal) : base(principal)
        {

        }
        public Guid ManagementLineId { get; set; }
        public string ManagementLineName { get; set; }
        public string ManagementLineDescription { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}