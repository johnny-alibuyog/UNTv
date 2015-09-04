using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ReactiveUI;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reactive;
using System.Reactive.Linq;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Televisions
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

                block(this.OneWayBind(ViewModel, x => x.MondayPrograms, x => x.MondayProgramsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.TuesdayPrograms, x => x.TuesdayProgramsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.WednesdayPrograms, x => x.WednesdayProgramsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.ThursdayPrograms, x => x.ThursdayProgramsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.FridayPrograms, x => x.FridayProgramsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.SaturdayPrograms, x => x.SaturdayProgramsListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.SundayPrograms, x => x.SundayProgramsListView.ItemsSource));

                BindClickEvent(this.MondayProgramsListView);
                BindClickEvent(this.TuesdayProgramsListView);
                BindClickEvent(this.WednesdayProgramsListView);
                BindClickEvent(this.ThursdayProgramsListView);
                BindClickEvent(this.FridayProgramsListView);
                BindClickEvent(this.SaturdayProgramsListView);
                BindClickEvent(this.SundayProgramsListView);

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
