using ReactiveUI;
using Splat;
using UNTv.WP81.DataProviders.Services;
using UNTv.WP81.Features;
using UNTv.WP81.Features.Controls.ListItemControls;
using UNTv.WP81.Features.News;
using UNTv.WP81.Features.Videos;

namespace UNTv.WP81
{
    public static class Bootstrapper
    {
        public static void Start()
        {
            //Locator.CurrentMutable = DependencyResolverFactory.Create();

            RxApp.SuspensionHost.CreateNewAppState = () => Locator.CurrentMutable.GetService<IScreen>();
            RxApp.SuspensionHost.SetupDefaultSuspendResume();

            var router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(router, typeof(RoutingState));
            Locator.CurrentMutable.RegisterConstant(new ShellViewModel(router), typeof(IScreen));

            // Web Services
            Locator.CurrentMutable.Register(() => new WebTvService(), typeof(IWebTvService));

            // Pages
            Locator.CurrentMutable.Register(() => new ShellView(), typeof(IViewFor<ShellViewModel>));
            Locator.CurrentMutable.Register(() => new MainHubView(), typeof(IViewFor<MainHubViewModel>));
            Locator.CurrentMutable.RegisterConstant(new NewsHubView(), typeof(IViewFor<NewsHubViewModel>));
            Locator.CurrentMutable.RegisterConstant(new NewsSectionView(), typeof(IViewFor<NewsSectionViewModel>));
            Locator.CurrentMutable.RegisterConstant(new VideosHubView(), typeof(IViewFor<VideosHubViewModel>));
            Locator.CurrentMutable.RegisterConstant(new VideosSectionView(), typeof(IViewFor<VideosSectionViewModel>));

            // Controls
            Locator.CurrentMutable.Register(() => new DetailView(), typeof(IViewFor<DetailViewModel>));
            Locator.CurrentMutable.Register(() => new VideoPlayerView(), typeof(IViewFor<VideoPlayerViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoLeftView(), typeof(IViewFor<PhotoLeftViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoColumns3View(), typeof(IViewFor<PhotoColumns3ViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoColumns4View(), typeof(IViewFor<PhotoColumns4ViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoSizeLargeView(), typeof(IViewFor<PhotoSizeLargeViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoSizeMediumView(), typeof(IViewFor<PhotoSizeMediumViewModel>));
        }
    }
}
