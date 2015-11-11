using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
                Func<IEnumerable<ReactiveBase>> GetVisibleSections = () => this.SectionHub.SectionsInView.Select(x => x.DataContext as ReactiveBase);

                // capture SectionInViewChanged event
                this.SectionHub.Events().SectionsInViewChanged
                    .Where(x => GetVisibleSections().Count() == 2)
                    .Subscribe(x => this.ViewModel.CurrentSection = GetVisibleSections().First());

                this.DataContext = this.ViewModel;

                this.ViewModel.PopulateCommand.Execute(null);
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
