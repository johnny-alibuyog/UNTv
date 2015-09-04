using System;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.DataProviders.Contracts.Messages;
using UNTv.WP81.DataProviders.Contracts.Services;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Radios
{
    public class ScheduleHubViewModel : ReactiveRoutableBase
    {
        private readonly RoutingState _router;
        private readonly IRadioService _service;

        public virtual ReactiveList<ItemViewModel> MondayPrograms { get; set; }
        public virtual ReactiveList<ItemViewModel> TuesdayPrograms { get; set; }
        public virtual ReactiveList<ItemViewModel> WednesdayPrograms { get; set; }
        public virtual ReactiveList<ItemViewModel> ThursdayPrograms { get; set; }
        public virtual ReactiveList<ItemViewModel> FridayPrograms { get; set; }
        public virtual ReactiveList<ItemViewModel> SaturdayPrograms { get; set; }
        public virtual ReactiveList<ItemViewModel> SundayPrograms { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToProgramsDetailCommand { get; private set; }

        public ScheduleHubViewModel(IScreen hostScreen = null)
            : base(hostScreen)
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IRadioService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToProgramsDetailCommand = ReactiveCommand.Create();
            this.NavigateToProgramsDetailCommand.Subscribe(x => NavigateToProgramsDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            _service.Get(new RadioProgramSchedulesRequest()).ContinueWith(
                continuationAction: x => this.ParseResult(x.Result),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void ParseResult(RadioProgramSchedulesResponse result)
        {
            this.MondayPrograms = result.AsItems(DayOfWeek.Monday);
            this.TuesdayPrograms = result.AsItems(DayOfWeek.Tuesday);
            this.WednesdayPrograms = result.AsItems(DayOfWeek.Wednesday);
            this.ThursdayPrograms = result.AsItems(DayOfWeek.Thursday);
            this.FridayPrograms = result.AsItems(DayOfWeek.Friday);
            this.SaturdayPrograms = result.AsItems(DayOfWeek.Saturday);
            this.SundayPrograms = result.AsItems(DayOfWeek.Sunday);
        }

        private void NavigateToProgramsDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: newsItem));
        }
    }
}
