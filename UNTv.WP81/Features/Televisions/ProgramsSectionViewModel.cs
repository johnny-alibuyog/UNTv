using System;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.DataProviders.Contracts.Messages;
using UNTv.WP81.DataProviders.Contracts.Services;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Televisions
{
    public class ProgramsSectionViewModel : ReactiveBase
    {
        private readonly RoutingState _router;
        private readonly ITelevisionService _service;

        public virtual ReactiveList<ItemViewModel> Programs { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToSchedulesHubCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToProgramDetailCommand { get; private set; }

        public ProgramsSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<ITelevisionService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToSchedulesHubCommand = ReactiveCommand.Create();
            this.NavigateToSchedulesHubCommand.Subscribe(x => NavigateToSchedulesHub());

            this.NavigateToProgramDetailCommand = ReactiveCommand.Create();
            this.NavigateToProgramDetailCommand.Subscribe(x => NavigateToVideosDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            _service.Get(new TelevisionProgramsRequest()).ContinueWith(
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
