using ReactiveUI;
using Splat;

namespace UNTv.WP81.Features
{
    public class ReactiveRoutableBase : ReactiveBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment { get; protected set; }

        public ReactiveRoutableBase(IScreen hostScreen = null)
        {
            this.HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
