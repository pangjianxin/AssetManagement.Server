using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class StoreAssetWithOutFileCommand : AssetCommand
    {
        public StoreAssetWithOutFileCommand(string assetName, string brand, string assetDescription,
            string assetType, string assetLocation, Guid assetCategoryId, DateTime createDateTime,
           string startTagNumber, string endTagNumber)
        {
            AssetName = assetName;
            Brand = brand;
            AssetDescription = assetDescription;
            AssetType = assetType;
            AssetLocation = assetLocation;
            AssetCategoryId = assetCategoryId;
            CreateDateTime = createDateTime;
            StartTagNumber = startTagNumber;
            EndTagNumber = endTagNumber;
        }
        public string StartTagNumber { get; set; }
        public string EndTagNumber { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new StoreAssetWithOutFileCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}