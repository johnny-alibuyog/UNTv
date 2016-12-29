using ReactiveUI;
using System;
using System.Reactive.Linq;
using UNTv.WP81.Features.Controls.ListItemControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Videos
{
    public sealed partial class VideosSectionView : UserControl, IViewFor<VideosSectionViewModel>
    {
        private bool _isActivated;

        public VideosSectionView()
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

                this.BindCommand(ViewModel, x => x.NavigateToVideosHubCommand, x => x.NavigateToVideosHubButton);
                this.Bind(ViewModel, x => x.Videos, x => x.VideosListView.ItemsSource);

                this.VideosListView.Events().ItemClick
                    .Select(x => x.ClickedItem as ItemViewModel)
                    .Where(x => this.ViewModel.NavigateToVideosDetailCommand.CanExecute(null))
                    .Subscribe(x => this.ViewModel.NavigateToVideosDetailCommand.Execute(x));

                this.ViewModel.PopulateCommand.Execute(null);

                _isActivated = true;
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
             .Register("ViewModel", typeof(VideosSectionViewModel), typeof(VideosSectionView), new PropertyMetadata(null));

        public VideosSectionViewModel ViewModel
        {
            get { return (VideosSectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (VideosSectionViewModel)value; }
        }
    }
}
