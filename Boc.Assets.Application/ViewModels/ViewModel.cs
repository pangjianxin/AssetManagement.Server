using Boc.Assets.Domain.Core.SharedKernel;

namespace Boc.Assets.Application.ViewModels
{
    public abstract class ViewModel
    {
        public IUser Principal { get; set; }
    }
}