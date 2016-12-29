using MyToolkit.Multimedia;
using ReactiveUI;
using Splat;
using System;
using System.Threading.Tasks;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Features.Controls.ListItemControls;
using UNTv.WP81.Features.Controls.MediaPlayerControls.Videos;

namespace UNTv.WP81.Features.Videos
{
    public class VideosSectionViewModel : ReactiveBase
    {
        private readonly RoutingState _router;
        private readonly IDataService _service;

        public virtual ReactiveList<ItemViewModel> Videos { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToVideosHubCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToVideosDetailCommand { get; private set; }

        public VideosSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IDataService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToVideosHubCommand = ReactiveCommand.Create();
            this.NavigateToVideosHubCommand.Subscribe(x => NavigateToVideosHub());

            this.NavigateToVideosDetailCommand = ReactiveCommand.Create();
            this.NavigateToVideosDetailCommand.Subscribe(x => NavigateToVideosDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            if (this.Videos.IsNullOrEmpty())
            {
                _service.Get(new VideoMessage.Request(SortFilter.Latest)).ContinueWith(
                    continuationAction: x => this.Videos = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }

        private void NavigateToVideosHub()
        {
            _router.Navigate.Execute(Locator.CurrentMutable.GetService<VideosHubViewModel>());
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
