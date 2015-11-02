using System;
using ReactiveUI;
using Splat;
using UNTv.WP81.Features.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UNTv.WP81.Features
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainHubView : Page, IViewFor<MainHubViewModel>
    {
        //private static int _currentHubIndex;

        public MainHubView()
        {
            this.InitializeComponent();
            this.InitializeBindings();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void InitializeBindings()
        {
            this.WhenActivated(block =>
            {
                this.SectionHub.Events().SectionsInViewChanged
                    .Subscribe(x => this.ViewModel.PopulateCommand.Execute(null));

                this.DataContext = this.ViewModel;
                //this.ViewModel.InitializeCommand.Execute(null);
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(MainHubViewModel), typeof(MainHubView), new PropertyMetadata(null));

        public MainHubViewModel ViewModel
        {
            get { return (MainHubViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainHubViewModel)value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var router = Locator.CurrentMutable.GetService<RoutingState>();
            var navigationRoute = new HlsPlayerViewModel();

            //navigationRoute.VideoUri = new Uri("http://devstreaming.apple.com/videos/wwdc/2015/105ncyldc6ofunvsgtan/105/hls_vod_mvp.m3u8");
            //navigationRoute.VideoUri = new Uri("http://devimages.apple.com/iphone/samples/bipbop/bipbopall.m3u8");
            navigationRoute.VideoUri = new Uri("http://livestream01.untvweb.com:1935/public/untvwebstream/playlist.m3u8");
            router.Navigate.Execute(navigationRoute);

        }
    }
}
