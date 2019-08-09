using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class AssetEventRevokeValidator:AssetValidator<Revoke>
    {
        public AssetEventRevokeValidator()
        {
            ValidateEventId();
            ValidateMessage();
        }
    }
}