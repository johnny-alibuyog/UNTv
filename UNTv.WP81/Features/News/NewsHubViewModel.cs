using System;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Data.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.News
{
    public class NewsHubViewModel : ReactiveRoutableBase
    {
        private readonly IStore _webStore;
        private readonly IStore _localStore;
        private readonly RoutingState _router;

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


        public NewsHubViewModel(IScreen hostScreen = null)
            : base(hostScreen)
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _webStore = Locator.CurrentMutable.GetService<WebStore>();
            _localStore = Locator.CurrentMutable.GetService<LocalStore>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToNewsDetailCommand = ReactiveCommand.Create();
            this.NavigateToNewsDetailCommand.Subscribe(x => NavigateToNewsDetail((ItemViewModel)x));

            //this.WhenNavigatedTo(() =>
            //{
            //    Populate();
            //    return null;
            //});
        }

        private void Populate()
        {
            //Task.Factory.StartNew(() => Populate(_localStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            //if (!NetworkInterface.GetIsNetworkAvailable())
            //    return;

            //Task.Factory.StartNew(() => Populate(_webStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            //Populate(_localStore);
            Populate(_webStore);
        }

        private void Populate(IStore store)
        {
            if (this.Headlines == null || this.Headlines.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.Headlines)).ContinueWith(
                    continuationAction: x => this.Headlines = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.WorldNews == null || this.WorldNews.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.World)).ContinueWith(
                    continuationAction: x => this.WorldNews = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.SportsNews == null || this.SportsNews.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.Sports)).ContinueWith(
                    continuationAction: x => this.SportsNews = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.HealthNews == null || this.HealthNews.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.Health)).ContinueWith(
                    continuationAction: x => this.HealthNews = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.PoliticalNews == null || this.PoliticalNews.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.Political)).ContinueWith(
                    continuationAction: x => this.PoliticalNews = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.EducationNews == null || this.EducationNews.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.Education)).ContinueWith(
                    continuationAction: x => this.EducationNews = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.TechnologyNews == null || this.TechnologyNews.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.Technology)).ContinueWith(
                    continuationAction: x => this.TechnologyNews = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.GovernmentNews == null || this.GovernmentNews.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.Government)).ContinueWith(
                    continuationAction: x => this.GovernmentNews = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.ProvincialNews == null || this.ProvincialNews.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.ProvincialNews)).ContinueWith(
                    continuationAction: x => this.ProvincialNews = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.ScienceNews == null || this.ScienceNews.Count == 0)
            {
                store.Get(new NewsMessage.Request(Category.ScienceAndEnvironment)).ContinueWith(
                    continuationAction: x => this.ScienceNews = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }

        private void NavigateToNewsDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: newsItem));
        }
    }
}
