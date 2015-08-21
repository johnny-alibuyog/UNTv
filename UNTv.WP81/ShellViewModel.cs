using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Features;
using UNTv.WP81.Features;

namespace UNTv.WP81
{
    public class ShellViewModel : ReactiveBase, IScreen
    {
        public RoutingState Router { get; protected set; }

        public ShellViewModel(RoutingState router = null)
        {
            this.Router = router;
            this.Router.Navigate.Execute(new HubPageViewModel(this));
        }
    }
}
