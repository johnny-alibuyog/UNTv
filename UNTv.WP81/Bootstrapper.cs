using ReactiveUI;
using Splat;
using UNTv.WP81.DataProviders.Contracts.Services;
using UNTv.WP81.Features;
using UNTv.WP81.Features.Controls.ListItemControls;
using UNTv.WP81.Features.News;
using UNTv.WP81.Features.Videos;
using UNTv.WP81.Features.PublicServices;
using Radio = UNTv.WP81.Features.Radios;
using Television = UNTv.WP81.Features.Televisions;
using UNTv.WP81.Features.Controls;

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
            Locator.CurrentMutable.Register(() => new RadioService(), typeof(IRadioService));
            Locator.CurrentMutable.Register(() => new TelevisionService(), typeof(ITelevisionService));

            // Pages
            Locator.CurrentMutable.Register(() => new ShellView(), typeof(IViewFor<ShellViewModel>));
            Locator.CurrentMutable.Register(() => new MainHubView(), typeof(IViewFor<MainHubViewModel>));
            Locator.CurrentMutable.RegisterConstant(new NewsHubView(), typeof(IViewFor<NewsHubViewModel>));
            Locator.CurrentMutable.RegisterConstant(new NewsSectionView(), typeof(IViewFor<NewsSectionViewModel>));
            Locator.CurrentMutable.RegisterConstant(new VideosHubView(), typeof(IViewFor<VideosHubViewModel>));
            Locator.CurrentMutable.RegisterConstant(new VideosSectionView(), typeof(IViewFor<VideosSectionViewModel>));
            Locator.CurrentMutable.RegisterConstant(new PublicServicesSectionView(), typeof(IViewFor<PublicServicesSectionViewModel>));
            Locator.CurrentMutable.RegisterConstant(new Radio.ScheduleHubView(), typeof(IViewFor<Radio.ScheduleHubViewModel>));
            Locator.CurrentMutable.RegisterConstant(new Radio.ProgramsSectionView(), typeof(IViewFor<Radio.ProgramsSectionViewModel>));
            Locator.CurrentMutable.RegisterConstant(new Television.ScheduleHubView(), typeof(IViewFor<Television.ScheduleHubViewModel>));
            Locator.CurrentMutable.RegisterConstant(new Television.ProgramsSectionView(), typeof(IViewFor<Television.ProgramsSectionViewModel>));

            // Controls
            Locator.CurrentMutable.Register(() => new DetailView(), typeof(IViewFor<DetailViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoLeftView(), typeof(IViewFor<PhotoLeftViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoColumns3View(), typeof(IViewFor<PhotoColumns3ViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoColumns4View(), typeof(IViewFor<PhotoColumns4ViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoSizeLargeView(), typeof(IViewFor<PhotoSizeLargeViewModel>));
            Locator.CurrentMutable.Register(() => new PhotoSizeMediumView(), typeof(IViewFor<PhotoSizeMediumViewModel>));
            Locator.CurrentMutable.Register(() => new HlsPlayerView(), typeof(IViewFor<HlsPlayerViewModel>));
            Locator.CurrentMutable.Register(() => new VideoPlayerView(), typeof(IViewFor<VideoPlayerViewModel>));
        }
    }
}
