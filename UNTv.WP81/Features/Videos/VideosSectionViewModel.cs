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
    public class VideosSectionViewModel : ReactiveBase
    {
        private readonly RoutingState _router;
        private readonly ITelevisionService _service;

        public virtual ReactiveList<ItemViewModel> Videos { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToVideosHubCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToVideosDetailCommand { get; private set; }

        public VideosSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<ITelevisionService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToVideosHubCommand = ReactiveCommand.Create();
            this.NavigateToVideosHubCommand.Subscribe(x => NavigateToVideosHub());

            this.NavigateToVideosDetailCommand = ReactiveCommand.Create();
            this.NavigateToVideosDetailCommand.Subscribe(x => NavigateToVideosDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            _service.Get(new VideosRequest(SortFilter.Latest)).ContinueWith(
                continuationAction: x => this.Videos = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void NavigateToVideosHub()
        {
            _router.Navigate.Execute(new VideosHubViewModel());
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
