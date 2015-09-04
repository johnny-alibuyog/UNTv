using System;
using System.Threading.Tasks;
using MyToolkit.Multimedia;
using ReactiveUI;
using Splat;
using UNTv.WP81.DataProviders.Contracts.Messages;
using UNTv.WP81.DataProviders.Contracts.Services;
using UNTv.WP81.Features.Controls;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Videos
{
    public class VideosHubViewModel : ReactiveRoutableBase
    {
        private readonly RoutingState _router;
        private readonly ITelevisionService _service;

        public virtual ReactiveList<ItemViewModel> LatestVideos { get; set; }
        public virtual ReactiveList<ItemViewModel> FeaturedVideos { get; set; }
        public virtual ReactiveList<ItemViewModel> PopularVideos { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToVideosDetailCommand { get; private set; }


        public VideosHubViewModel(IScreen hostScreen = null, ITelevisionService service = null)
            : base(hostScreen)
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<ITelevisionService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToVideosDetailCommand = ReactiveCommand.Create();
            this.NavigateToVideosDetailCommand.Subscribe(x => NavigateToVideosDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            _service.Get(new VideosRequest(SortFilter.Latest)).ContinueWith(
                continuationAction: x => this.LatestVideos = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new VideosRequest(SortFilter.Featured)).ContinueWith(
                continuationAction: x => this.FeaturedVideos = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new VideosRequest(SortFilter.Popular)).ContinueWith(
                continuationAction: x => this.PopularVideos = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void NavigateToVideosDetail(ItemViewModel item)
        {
            var navigationPath = new VideoPlayerViewModel();

            YouTube.GetVideoUriAsync(item.Content, YouTubeQuality.Quality480P).ContinueWith(
                continuationAction: x => navigationPath.VideoUri = x.Result.Uri,
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _router.Navigate.Execute(navigationPath);
        }
    }
}
