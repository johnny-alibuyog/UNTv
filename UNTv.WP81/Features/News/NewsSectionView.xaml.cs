using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using UNTv.WP81.Features.Controls.ListItemControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.News
{
    public sealed partial class NewsSectionView : UserControl, IViewFor<NewsSectionViewModel>
    {
        public NewsSectionView()
        {
            this.InitializeComponent();
            this.InitializeBindings();
        }

        private void InitializeBindings()
        {
            this.WhenActivated(block =>
            {
                this.ViewModel = new NewsSectionViewModel();

                block(this.BindCommand(ViewModel, x => x.NavigateToNewsPageCommand, x => x.GoToNewsPage));
                block(this.OneWayBind(ViewModel, x => x.Headlines, x => x.HeadlinesListView.ItemsSource));

                this.HeadlinesListView.Events().ItemClick
                    .Select(x => x.ClickedItem as ItemViewModel)
                    .Where(x => this.ViewModel.NavigateToNewsDetailCommand.CanExecute(null))
                    .Subscribe(x => this.ViewModel.NavigateToNewsDetailCommand.Execute(x));
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
