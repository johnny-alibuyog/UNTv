using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Data.Entities;
using UNTv.WP81.Features;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81
{
    public class ShellViewModel : ReactiveBase, IScreen
    {
        public RoutingState Router { get; protected set; }

        //public virtual ItemViewModel Headline { get; set; }

        public ShellViewModel(RoutingState router = null)
        {
            this.Router = router;
            this.Router.Navigate.Execute(new MainHubViewModel(this));

            //var store = Locator.CurrentMutable.GetService<WebStore>();
            //store.Get(new NewsMessage.Request(Category.Headlines, 1)).ContinueWith(
            //    continuationAction: x => this.Headline = x.Result.AsItems().FirstOrDefault(),
            //    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //);
        }
    }
}
