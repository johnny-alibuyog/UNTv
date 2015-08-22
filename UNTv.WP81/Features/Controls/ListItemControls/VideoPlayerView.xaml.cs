using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class VideoPlayerView : Page, IViewFor<VideoPlayerViewModel>
    {
        public VideoPlayerView()
        {
            this.InitializeComponent();
            this.InitializeBinding();
        }

        private void InitializeBinding()
        {
            this.WhenActivated(block =>
            {
                //block(this.OneWayBind(ViewModel, x => x.Title, x => x.TitleTextBlock.Text));
                //block(this.Bind(ViewModel, x => x.Content, x => x.ContentGridView));

                DataContext = this.ViewModel;

                //this.WebView.NavigateToString(string.Format("<iframe width='620' height='348' src='{0}' frameborder='0'></iframe>", this.ViewModel.VideoUri));
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(VideoPlayerViewModel), typeof(VideoPlayerView), new PropertyMetadata(null));

        public VideoPlayerViewModel ViewModel
        {
            get { return (VideoPlayerViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (VideoPlayerViewModel)value; }
        }
    }
}
