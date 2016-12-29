using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Controls.MediaPlayerControls.Hls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HlsVideoPlayerView : Page, IViewFor<HlsVideoPlayerViewModel>
    {
        public HlsVideoPlayerView()
        {
            this.InitializeComponent();
            this.InitializeBinding();
        }

        private void InitializeBinding()
        {
            this.WhenActivated(block =>
            {
                DataContext = this.ViewModel;

                //this.ViewModel.WhenAnyValue(x => x.MediaType)
                //    .Subscribe(x =>
                //    {
                //        if (x == MediaType.Audio)
                //        {
                //            //var image = new Image()
                //            //{
                //            //    Source = new BitmapImage(new Uri(@"ms-appx://Assets/Images/UntvRadioLogo.png", UriKind.Absolute))
                //            //};

                //            var imageBrush = new ImageBrush()
                //            {
                //                ImageSource = new BitmapImage(new Uri("ms-appx://Assets/Images/UntvRadioLogo.png")),
                //                Stretch = Windows.UI.Xaml.Media.Stretch.None,
                //                AlignmentX = AlignmentX.Center,
                //                AlignmentY = AlignmentY.Center,
                //                Opacity = 0.3

                //                //AlignmentX = Windows.UI.Xaml.Media.AlignmentX.Center,
                //                //AlignmentY = Windows.UI.Xaml.Media.AlignmentY.Center,
                //                //Stretch = Windows.UI.Xaml.Media.Stretch.None,
                //                //ImageSource = image.Source
                //            };

                //            this.MainGrid.Background = imageBrush; //new SolidColorBrush(Windows.UI.Colors.Red); 
                //        }
                //        else
                //        {
                //            this.MainGrid.Background = null;
                //        }
                //    });
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(HlsVideoPlayerViewModel), typeof(HlsVideoPlayerView), new PropertyMetadata(null));

        public HlsVideoPlayerViewModel ViewModel
        {
            get { return (HlsVideoPlayerViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (HlsVideoPlayerViewModel)value; }
        }
    }
}
