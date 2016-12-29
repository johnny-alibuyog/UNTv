using ReactiveUI;
using UNTv.WP81.Features;

namespace UNTv.WP81
{
    public class ShellViewModel : ReactiveBase, IScreen
    {
        public RoutingState Router { get; protected set; }

        public ShellViewModel(RoutingState router = null)
        {
            this.Router = router;
            this.Router.Navigate.Execute(new MainHubViewModel(this));
        }
    }
}
