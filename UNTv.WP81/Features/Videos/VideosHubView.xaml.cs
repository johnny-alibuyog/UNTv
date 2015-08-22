using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using UNTv.WP81.Features.Controls.ListItemControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace UNTv.WP81.Features.Videos
{
    public sealed partial class VideosHubView : Page, IViewFor<VideosHubViewModel>
    {
        private bool _isActivated;

        public VideosHubView()
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

                Action<ListView> BindClickEvent = (listView) =>
                {
                    listView.Events().ItemClick
                        .Select(x => x.ClickedItem as ItemViewModel)
                        .Where(x => this.ViewModel.NavigateToVideosDetailCommand.CanExecute(null))
                        .Subscribe(x => this.ViewModel.NavigateToVideosDetailCommand.Execute(x));
                };

                block(this.OneWayBind(ViewModel, x => x.LatestVideos, x => x.LatestVideosListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.FeaturedVideos, x => x.FeaturedVideosListView.ItemsSource));
                block(this.OneWayBind(ViewModel, x => x.PopularVideos, x => x.PopularVideosListView.ItemsSource));

                BindClickEvent(this.LatestVideosListView);
                BindClickEvent(this.FeaturedVideosListView);
                BindClickEvent(this.PopularVideosListView);

                this.ViewModel.PopulateCommand.Execute(null);

                _isActivated = true;
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(VideosHubViewModel), typeof(VideosHubView), new PropertyMetadata(null));

        public VideosHubViewModel ViewModel
        {
            get { return (VideosHubViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (VideosHubViewModel)value; }
        }

    }
}
