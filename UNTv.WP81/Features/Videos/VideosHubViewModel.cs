using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.DataProviders.Services;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Videos
{
    public class VideosHubViewModel : ReactiveRoutableBase
    {
        private readonly RoutingState _router;
        private readonly IWebTvService _service;

        public virtual ReactiveList<ItemViewModel> LatestVideos { get; set; }
        public virtual ReactiveList<ItemViewModel> FeaturedVideos { get; set; }
        public virtual ReactiveList<ItemViewModel> PopularVideos { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToVideosDetailCommand { get; private set; }


        public VideosHubViewModel(IScreen hostScreen = null, IWebTvService service = null)
            : base(hostScreen)
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IWebTvService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToVideosDetailCommand = ReactiveCommand.Create();
            this.NavigateToVideosDetailCommand.Subscribe(x => NavigateToVideosDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            _service.Get(new TvProgramRequest(TvProgramRequest.Filter.Latest)).ContinueWith(
                continuationAction: x => this.LatestVideos = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new TvProgramRequest(TvProgramRequest.Filter.Featured)).ContinueWith(
                continuationAction: x => this.FeaturedVideos = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new TvProgramRequest(TvProgramRequest.Filter.Popular)).ContinueWith(
                continuationAction: x => this.PopularVideos = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void NavigateToVideosDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new VideoPlayerViewModel(value: newsItem));
        }
    }
}
