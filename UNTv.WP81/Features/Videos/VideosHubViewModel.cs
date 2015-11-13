using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reactive;
using System.Reactive.Linq;
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
        private readonly RoutingState _router;
        private readonly IDataService _service;

        public virtual bool IsLoading { get; set; }
        public virtual SortFilter CurrentSection { get; set; }
        public virtual ReactiveList<ItemViewModel> LatestVideos { get; set; }
        public virtual ReactiveList<ItemViewModel> FeaturedVideos { get; set; }
        public virtual ReactiveList<ItemViewModel> PopularVideos { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToVideosDetailCommand { get; private set; }


        public VideosHubViewModel(IScreen hostScreen = null)
            : base(hostScreen)
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IDataService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate(x as Nullable<SortFilter>));

            this.NavigateToVideosDetailCommand = ReactiveCommand.Create();
            this.NavigateToVideosDetailCommand.Subscribe(x => NavigateToVideosDetail((ItemViewModel)x));

            this.CurrentSection = SortFilter.Latest;
            this.WhenAnyValue(x => x.CurrentSection)
                .Subscribe(x => this.PopulateCommand.Execute(x));

            // Setup progress bar
            this.WhenAnyValue(x => x.LatestVideos)
                .Where(x => this.CurrentSection == SortFilter.Latest)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.FeaturedVideos)
                .Where(x => this.CurrentSection == SortFilter.Featured)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.PopularVideos)
                .Where(x => this.CurrentSection == SortFilter.Popular)
                .Subscribe(x => this.IsLoading = x == null);
        }

        private void Populate(Nullable<SortFilter> section = null)
        {
            Action<SortFilter, ReactiveList<ItemViewModel>, Action<ReactiveList<ItemViewModel>>> EvaluateThenPopulate = (sortFilter, items, callback) =>
            {
                if ((section == null || section == sortFilter) && (this.IsLoading = items.IsNullOrEmpty()))
                {
                    _service.Get(new VideoMessage.Request(sortFilter)).ContinueWith(
                        continuationAction: x => callback(x.Result.AsItems()),
                        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                    );
                }
            };

            EvaluateThenPopulate(SortFilter.Latest, this.LatestVideos, result => this.LatestVideos = result);
            EvaluateThenPopulate(SortFilter.Featured, this.FeaturedVideos, result => this.FeaturedVideos = result);
            EvaluateThenPopulate(SortFilter.Popular, this.PopularVideos, result => this.PopularVideos = result);
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
