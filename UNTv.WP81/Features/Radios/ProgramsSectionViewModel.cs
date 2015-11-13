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
        private readonly RoutingState _router;
        private readonly IDataService _service;

        public virtual ReactiveList<ItemViewModel> Programs { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToSchedulesHubCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToProgramDetailCommand { get; private set; }

        public ProgramsSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IDataService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToSchedulesHubCommand = ReactiveCommand.Create();
            this.NavigateToSchedulesHubCommand.Subscribe(x => NavigateToSchedulesHub());

            this.NavigateToProgramDetailCommand = ReactiveCommand.Create();
            this.NavigateToProgramDetailCommand.Subscribe(x => NavigateToVideosDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            if (this.Programs == null || this.Programs.Count == 0)
            {
                _service.Get(new RadioProgramMessage.Request()).ContinueWith(
                    continuationAction: x => this.Programs = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }

        private void NavigateToSchedulesHub()
        {
            _router.Navigate.Execute(Locator.CurrentMutable.GetService<ScheduleHubViewModel>());
        }

        private void NavigateToVideosDetail(ItemViewModel programItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: programItem));
        }
    }
}
