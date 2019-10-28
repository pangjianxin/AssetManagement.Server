using System;
using System.ComponentModel.DataAnnotations;

namespace Boc.Assets.Application.Dto
{
    public class AssetInventoryRegisterDto
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Progress { get; set; }

        public OrgDto Participation { get; set; }
        public AssetInventoryDto AssetInventory { get; set; }
    }
}