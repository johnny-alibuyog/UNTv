using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
        private readonly IDataService _service;
        private readonly RoutingState _router;

        public virtual bool IsLoading { get; set; }
        public virtual Nullable<DayOfWeek> CurrentSection { get; set; }
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
            _service = Locator.CurrentMutable.GetService<IDataService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate(x as Nullable<DayOfWeek>));

            this.NavigateToProgramsDetailCommand = ReactiveCommand.Create();
            this.NavigateToProgramsDetailCommand.Subscribe(x => NavigateToProgramsDetail((ItemViewModel)x));

            this.CurrentSection = DayOfWeek.Monday;
            this.WhenAnyValue(x => x.CurrentSection)
                .Subscribe(x => this.PopulateCommand.Execute(x));

            // Setup progress bar
            this.WhenAnyValue(x => x.MondayPrograms)
                .Where(x => this.CurrentSection == DayOfWeek.Monday)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.TuesdayPrograms)
                .Where(x => this.CurrentSection == DayOfWeek.Tuesday)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.WednesdayPrograms)
                .Where(x => this.CurrentSection == DayOfWeek.Wednesday)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.ThursdayPrograms)
                .Where(x => this.CurrentSection == DayOfWeek.Thursday)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.FridayPrograms)
                .Where(x => this.CurrentSection == DayOfWeek.Monday)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.SaturdayPrograms)
                .Where(x => this.CurrentSection == DayOfWeek.Saturday)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.SundayPrograms)
                .Where(x => this.CurrentSection == DayOfWeek.Sunday)
                .Subscribe(x => this.IsLoading = x == null);
        }

        private void Populate(Nullable<DayOfWeek> section = null)
        {
            var needsToPopulate = section == null ||
               ((section == DayOfWeek.Monday) && (this.IsLoading = this.MondayPrograms.IsNullOrEmpty())) ||
               ((section == DayOfWeek.Tuesday) && (this.IsLoading = this.TuesdayPrograms.IsNullOrEmpty())) ||
               ((section == DayOfWeek.Wednesday) && (this.IsLoading = this.MondayPrograms.IsNullOrEmpty())) ||
               ((section == DayOfWeek.Thursday) && (this.IsLoading = this.MondayPrograms.IsNullOrEmpty())) ||
               ((section == DayOfWeek.Friday) && (this.IsLoading = this.MondayPrograms.IsNullOrEmpty())) ||
               ((section == DayOfWeek.Saturday) && (this.IsLoading = this.MondayPrograms.IsNullOrEmpty())) ||
               ((section == DayOfWeek.Sunday) && (this.IsLoading = this.MondayPrograms.IsNullOrEmpty()));


            if (needsToPopulate)
            {
                _service.Get(new RadioProgramScheduleMessage.Request()).ContinueWith(
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
