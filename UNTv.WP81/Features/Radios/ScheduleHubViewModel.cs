using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Radios
{
    public class ScheduleHubViewModel : ReactiveRoutableBase
    {
        private readonly IStore _webStore;
        private readonly IStore _localStore;
        private readonly RoutingState _router;

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
            _webStore = Locator.CurrentMutable.GetService<WebStore>();
            _localStore = Locator.CurrentMutable.GetService<LocalStore>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToProgramsDetailCommand = ReactiveCommand.Create();
            this.NavigateToProgramsDetailCommand.Subscribe(x => NavigateToProgramsDetail((ItemViewModel)x));
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
            var needsToPopulate =
               this.MondayPrograms.IsNullOrEmpty() ||
               this.TuesdayPrograms.IsNullOrEmpty() ||
               this.WednesdayPrograms.IsNullOrEmpty() ||
               this.ThursdayPrograms.IsNullOrEmpty() ||
               this.FridayPrograms.IsNullOrEmpty() ||
               this.SaturdayPrograms.IsNullOrEmpty() ||
               this.SundayPrograms.IsNullOrEmpty();

            if (needsToPopulate)
            {
                store.Get(new RadioProgramScheduleMessage.Request()).ContinueWith(
                    continuationAction: x => this.ParseResult(x.Result),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }

        private void ParseResult(RadioProgramScheduleMessage.Response result)
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
