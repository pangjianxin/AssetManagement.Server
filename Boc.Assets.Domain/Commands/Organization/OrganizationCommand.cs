﻿using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.SharedKernel;

namespace Boc.Assets.Domain.Commands.Organization
{
    public abstract class OrganizationCommand : Command
    {
        public OrganizationCommand(IUser principal) : base(principal)
        {

        }
        public string OrgIdentifier { get; set; }
        public string OrgNam { get; set; }
    }
}