namespace Boc.Assets.Application.ViewModels.Organization
{
    public class ChangeOrgPassword : OrganizationViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}