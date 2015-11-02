using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using MyToolkit.Multimedia;
using ReactiveUI;
using Splat;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Features.Controls;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Videos
{
    public class VideosHubViewModel : ReactiveRoutableBase
    {
        private readonly IStore _webStore;
        private readonly IStore _localStore;
        private readonly RoutingState _router;

        public virtual ReactiveList<ItemViewModel> LatestVideos { get; set; }
        public virtual ReactiveList<ItemViewModel> FeaturedVideos { get; set; }
        public virtual ReactiveList<ItemViewModel> PopularVideos { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToVideosDetailCommand { get; private set; }


        public VideosHubViewModel(IScreen hostScreen = null)
            : base(hostScreen)
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _webStore = Locator.CurrentMutable.GetService<WebStore>();
            _localStore = Locator.CurrentMutable.GetService<LocalStore>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToVideosDetailCommand = ReactiveCommand.Create();
            this.NavigateToVideosDetailCommand.Subscribe(x => NavigateToVideosDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            //Task.Factory.StartNew(() => Populate(_localStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            //if (!NetworkInterface.GetIsNetworkAvailable())
            //    return;`

            //Task.Factory.StartNew(() => Populate(_webStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            Populate(_webStore);
        }

        private void Populate(IStore store)
        {
            if (this.LatestVideos.IsNullOrEmpty())
            {
                store.Get(new VideoMessage.Request(SortFilter.Latest)).ContinueWith(
                    continuationAction: x => this.LatestVideos = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.FeaturedVideos.IsNullOrEmpty())
            {
                store.Get(new VideoMessage.Request(SortFilter.Featured)).ContinueWith(
                    continuationAction: x => this.FeaturedVideos = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.PopularVideos.IsNullOrEmpty())
            {
                store.Get(new VideoMessage.Request(SortFilter.Popular)).ContinueWith(
                    continuationAction: x => this.PopularVideos = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
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
