using System;
using System.Reactive.Linq;
using ReactiveUI;
using UNTv.WP81.Features.Controls.ListItemControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.News
{
    public sealed partial class NewsSectionView : UserControl, IViewFor<NewsSectionViewModel>
    {
        private bool _isActivated;

        public NewsSectionView()
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

                this.BindCommand(ViewModel, x => x.NavigateToNewsHubCommand, x => x.NavigateToNewsHubButton);
                block(this.OneWayBind(ViewModel, x => x.News, x => x.NewsListView.ItemsSource));

                this.NewsListView.Events().ItemClick
                    .Select(x => x.ClickedItem as ItemViewModel)
                    .Where(x => this.ViewModel.NavigateToNewsDetailCommand.CanExecute(null))
                    .Subscribe(x => this.ViewModel.NavigateToNewsDetailCommand.Execute(x));

                this.ViewModel.PopulateCommand.Execute(null);

                _isActivated = true;
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                .Register("ViewModel", typeof(NewsSectionViewModel), typeof(NewsSectionView), new PropertyMetadata(null));

        public NewsSectionViewModel ViewModel
        {
            get { return (NewsSectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (NewsSectionViewModel)value; }
        }
    }
}
