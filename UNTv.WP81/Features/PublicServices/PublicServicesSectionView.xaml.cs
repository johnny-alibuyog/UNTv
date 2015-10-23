using System;
using System.Reactive.Linq;
using ReactiveUI;
using UNTv.WP81.Features.Controls.ListItemControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.PublicServices
{
    public sealed partial class PublicServicesSectionView : UserControl, IViewFor<PublicServicesSectionViewModel>
    {
        private bool _isActivated;

        public PublicServicesSectionView()
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

                block(this.OneWayBind(ViewModel, x => x.Programs, x => x.ProgramsListView.ItemsSource));

                this.ProgramsListView.Events().ItemClick
                    .Select(x => x.ClickedItem as ItemViewModel)
                    .Where(x => this.ViewModel.NavigateToProgramsDetailCommand.CanExecute(null))
                    .Subscribe(x => this.ViewModel.NavigateToProgramsDetailCommand.Execute(x));

                this.ViewModel.PopulateCommand.Execute(null);

                _isActivated = true;
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
             .Register("ViewModel", typeof(PublicServicesSectionViewModel), typeof(PublicServicesSectionView), new PropertyMetadata(null));

        public PublicServicesSectionViewModel ViewModel
        {
            get { return (PublicServicesSectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PublicServicesSectionViewModel)value; }
        }
    }
}
