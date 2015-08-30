using System;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.DataProviders.Models;
using UNTv.WP81.DataProviders.Services;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.News
{
    public class NewsHubViewModel : ReactiveRoutableBase
    {
        private readonly RoutingState _router;
        private readonly ITelevisionService _service;

        public virtual ReactiveList<ItemViewModel> Headlines { get; set; }
        public virtual ReactiveList<ItemViewModel> WorldNews { get; set; }
        public virtual ReactiveList<ItemViewModel> SportsNews { get; set; }
        public virtual ReactiveList<ItemViewModel> HealthNews { get; set; }
        public virtual ReactiveList<ItemViewModel> PoliticalNews { get; set; }
        public virtual ReactiveList<ItemViewModel> EducationNews { get; set; }
        public virtual ReactiveList<ItemViewModel> TechnologyNews { get; set; }
        public virtual ReactiveList<ItemViewModel> GovernmentNews { get; set; }
        public virtual ReactiveList<ItemViewModel> ProvincialNews { get; set; }
        public virtual ReactiveList<ItemViewModel> ScienceNews { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToNewsDetailCommand { get; private set; }


        public NewsHubViewModel(IScreen hostScreen = null, ITelevisionService service = null)
            : base(hostScreen)
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<ITelevisionService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToNewsDetailCommand = ReactiveCommand.Create();
            this.NavigateToNewsDetailCommand.Subscribe(x => NavigateToNewsDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            _service.Get(new NewsRequest(Category.Headlines)).ContinueWith(
                continuationAction: x => this.Headlines = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new NewsRequest(Category.World)).ContinueWith(
                continuationAction: x => this.WorldNews = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new NewsRequest(Category.Sports)).ContinueWith(
                continuationAction: x => this.SportsNews = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new NewsRequest(Category.Health)).ContinueWith(
                continuationAction: x => this.HealthNews = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new NewsRequest(Category.Political)).ContinueWith(
                continuationAction: x => this.PoliticalNews = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new NewsRequest(Category.Education)).ContinueWith(
                continuationAction: x => this.EducationNews = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new NewsRequest(Category.Technology)).ContinueWith(
                continuationAction: x => this.TechnologyNews = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new NewsRequest(Category.Government)).ContinueWith(
                continuationAction: x => this.GovernmentNews = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new NewsRequest(Category.ProvincialNews)).ContinueWith(
                continuationAction: x => this.ProvincialNews = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );

            _service.Get(new NewsRequest(Category.ScienceAndEnvironment)).ContinueWith(
                continuationAction: x => this.ScienceNews = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void NavigateToNewsDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: newsItem));
        }
    }
}
