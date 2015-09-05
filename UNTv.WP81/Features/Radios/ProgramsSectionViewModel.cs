using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Radios
{
    public class ProgramsSectionViewModel : ReactiveBase
    {
        private readonly IStore _webStore;
        private readonly IStore _localStore;
        private readonly RoutingState _router;

        public virtual ReactiveList<ItemViewModel> Programs { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToSchedulesHubCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToProgramDetailCommand { get; private set; }

        public ProgramsSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _webStore = Locator.CurrentMutable.GetService<WebStore>();
            _localStore = Locator.CurrentMutable.GetService<LocalStore>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToSchedulesHubCommand = ReactiveCommand.Create();
            this.NavigateToSchedulesHubCommand.Subscribe(x => NavigateToSchedulesHub());

            this.NavigateToProgramDetailCommand = ReactiveCommand.Create();
            this.NavigateToProgramDetailCommand.Subscribe(x => NavigateToVideosDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            //Task.Factory.StartNew(() => Populate(_localStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            //if (!NetworkInterface.GetIsNetworkAvailable())
            //    return;

            //Task.Factory.StartNew(() => Populate(_webStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            Populate(_webStore);
        }

        private void Populate(IStore store)
        {
            store.Get(new RadioProgramMessage.Request()).ContinueWith(
                continuationAction: x => this.Programs = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void NavigateToSchedulesHub()
        {
            _router.Navigate.Execute(new ScheduleHubViewModel());
        }

        private void NavigateToVideosDetail(ItemViewModel programItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: programItem));
        }
    }
}
