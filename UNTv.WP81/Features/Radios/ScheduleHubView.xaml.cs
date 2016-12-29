using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Features.Controls.ListItemControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Radios
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScheduleHubView : Page, IViewFor<ScheduleHubViewModel>
    {
        private bool _isActivated;

        public ScheduleHubView()
        {
            this.InitializeComponent();
            this.InitializeBindint();
        }

        private void InitializeBindint()
        {
            this.WhenActivated(block =>
            {
                if (_isActivated)
                    return;

                Action<ListView> BindClickEvent = (listView) =>
                {
                    listView.Events().ItemClick
                        .Select(x => x.ClickedItem as ItemViewModel)
                        .Where(x => this.ViewModel.NavigateToProgramsDetailCommand.CanExecute(null))
                        .Subscribe(x => this.ViewModel.NavigateToProgramsDetailCommand.Execute(x));
                };

                Action<PivotItem> SetCurrentSection = (pivotItem) =>
                {
                    var stringValue = pivotItem.Header.ToString().ToProperCase();
                    var enumValue = default(DayOfWeek);

                    if (Enum.TryParse<DayOfWeek>(stringValue, out enumValue))
                        this.ViewModel.CurrentSection = enumValue;
                };

                this.SchedulePivot.Events().SelectionChanged
                    .Select(x => x.AddedItems.OfType<PivotItem>().FirstOrDefault())
                    .Where(pivotItem => pivotItem != null)
                    .Subscribe(x => SetCurrentSection(x));

                this.Bind(ViewModel, x => x.IsLoading, x => x.ProgressBar.IsIndeterminate);
                this.Bind(ViewModel, x => x.MondayPrograms, x => x.MondayProgramsListView.ItemsSource);
                this.Bind(ViewModel, x => x.TuesdayPrograms, x => x.TuesdayProgramsListView.ItemsSource);
                this.Bind(ViewModel, x => x.WednesdayPrograms, x => x.WednesdayProgramsListView.ItemsSource);
                this.Bind(ViewModel, x => x.ThursdayPrograms, x => x.ThursdayProgramsListView.ItemsSource);
                this.Bind(ViewModel, x => x.FridayPrograms, x => x.FridayProgramsListView.ItemsSource);
                this.Bind(ViewModel, x => x.SaturdayPrograms, x => x.SaturdayProgramsListView.ItemsSource);
                this.Bind(ViewModel, x => x.SundayPrograms, x => x.SundayProgramsListView.ItemsSource);

                BindClickEvent(this.MondayProgramsListView);
                BindClickEvent(this.TuesdayProgramsListView);
                BindClickEvent(this.WednesdayProgramsListView);
                BindClickEvent(this.ThursdayProgramsListView);
                BindClickEvent(this.FridayProgramsListView);
                BindClickEvent(this.SaturdayProgramsListView);
                BindClickEvent(this.SundayProgramsListView);

                this.SchedulePivot.Events().SelectionChanged
                    .Subscribe(x => this.ViewModel.PopulateCommand.Execute(null));

                this.ViewModel.PopulateCommand.Execute(null);

                _isActivated = true;
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
               .Register("ViewModel", typeof(ScheduleHubViewModel), typeof(ScheduleHubView), new PropertyMetadata(null));

        public ScheduleHubViewModel ViewModel
        {
            get { return (ScheduleHubViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ScheduleHubViewModel)value; }
        }
    }
}
