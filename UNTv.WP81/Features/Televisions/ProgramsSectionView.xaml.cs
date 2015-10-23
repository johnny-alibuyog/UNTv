using System;
using System.Reactive.Linq;
using ReactiveUI;
using UNTv.WP81.Features.Controls.ListItemControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Televisions
{
    public sealed partial class ProgramsSectionView : UserControl, IViewFor<ProgramsSectionViewModel>
    {
        private bool _isActivated;

        public ProgramsSectionView()
        {
            this.InitializeComponent();
            this.InitializeBindings();
        }

        private void InitializeBindings()
        {
            this.WhenActivated(block =>
            {
                if (_isActivated)
                    return;

                this.BindCommand(ViewModel, x => x.NavigateToSchedulesHubCommand, x => x.NavigateToSchedulesHubButton);
                block(this.OneWayBind(ViewModel, x => x.Programs, x => x.ProgramsListView.ItemsSource));

                this.ProgramsListView.Events().ItemClick
                    .Select(x => x.ClickedItem as ItemViewModel)
                    .Where(x => this.ViewModel.NavigateToProgramDetailCommand.CanExecute(null))
                    .Subscribe(x => this.ViewModel.NavigateToProgramDetailCommand.Execute(x));

                this.ViewModel.PopulateCommand.Execute(null);

                _isActivated = true;
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
             .Register("ViewModel", typeof(ProgramsSectionViewModel), typeof(ProgramsSectionView), new PropertyMetadata(null));

        public ProgramsSectionViewModel ViewModel
        {
            get { return (ProgramsSectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ProgramsSectionViewModel)value; }
        }
    }
}
