using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Common.Extentions;
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

        public virtual bool IsLoading { get; set; }
        public virtual Category CurrentSection { get; set; }
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
            this.PopulateCommand.Subscribe(x => Populate(x as Category));

            this.NavigateToNewsDetailCommand = ReactiveCommand.Create();
            this.NavigateToNewsDetailCommand.Subscribe(x => NavigateToNewsDetail((ItemViewModel)x));


            this.CurrentSection = Category.Headlines;
            this.WhenAnyValue(x => x.CurrentSection)
                .Subscribe(x => this.PopulateCommand.Execute(x));

            // Setup progress bar
            this.WhenAnyValue(x => x.Headlines)
                .Where(x => this.CurrentSection == Category.Headlines)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.WorldNews)
                .Where(x => this.CurrentSection == Category.World)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.HealthNews)
                .Where(x => this.CurrentSection == Category.Health)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.PoliticalNews)
                .Where(x => this.CurrentSection == Category.Political)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.EducationNews)
                .Where(x => this.CurrentSection == Category.Education)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.TechnologyNews)
                .Where(x => this.CurrentSection == Category.Technology)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.GovernmentNews)
                .Where(x => this.CurrentSection == Category.Government)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.ProvincialNews)
                .Where(x => this.CurrentSection == Category.ProvincialNews)
                .Subscribe(x => this.IsLoading = x == null);

            this.WhenAnyValue(x => x.ScienceNews)
                .Where(x => this.CurrentSection == Category.ScienceAndEnvironment)
                .Subscribe(x => this.IsLoading = x == null);

        }

        private void Populate(Category section = null)
        {
            //Task.Factory.StartNew(() => Populate(_localStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            //if (!NetworkInterface.GetIsNetworkAvailable())
            //    return;

            //Task.Factory.StartNew(() => Populate(_webStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            //Populate(_localStore);
            Populate(_webStore, section);
        }

        private void Populate(IStore store, Category section = null)
        {
            Action<Category, ReactiveList<ItemViewModel>, Action<ReactiveList<ItemViewModel>>> EvaluateThenPopulate = (category, items, callback) =>
            {
                if ((section == null || section == category) && (this.IsLoading = items.IsNullOrEmpty()))
                {
                    store.Get(new NewsMessage.Request(category)).ContinueWith(
                        continuationAction: x => callback(x.Result.AsItems()),
                        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                    );
                }
            };

            EvaluateThenPopulate(Category.Headlines, this.Headlines, result => this.Headlines = result);
            EvaluateThenPopulate(Category.World, this.WorldNews, result => this.WorldNews = result);
            EvaluateThenPopulate(Category.Sports, this.SportsNews, result => this.SportsNews = result);
            EvaluateThenPopulate(Category.Health, this.HealthNews, result => this.HealthNews = result);
            EvaluateThenPopulate(Category.Political, this.PoliticalNews, result => this.PoliticalNews = result);
            EvaluateThenPopulate(Category.Education, this.EducationNews, result => this.EducationNews = result);
            EvaluateThenPopulate(Category.Technology, this.TechnologyNews, result => this.TechnologyNews = result);
            EvaluateThenPopulate(Category.Government, this.GovernmentNews, result => this.GovernmentNews = result);
            EvaluateThenPopulate(Category.ProvincialNews, this.ProvincialNews, result => this.ProvincialNews = result);
            EvaluateThenPopulate(Category.ScienceAndEnvironment, this.ScienceNews, result => this.ScienceNews = result);

            //if (this.IsLoading = this.Headlines.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.Headlines)).ContinueWith(
            //        continuationAction: x => this.Headlines = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}

            //if (this.IsLoading = this.WorldNews.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.World)).ContinueWith(
            //        continuationAction: x => this.WorldNews = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}

            //if (this.IsLoading = this.SportsNews.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.Sports)).ContinueWith(
            //        continuationAction: x => this.SportsNews = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}

            //if (this.IsLoading = this.HealthNews.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.Health)).ContinueWith(
            //        continuationAction: x => this.HealthNews = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}

            //if (this.IsLoading = this.PoliticalNews.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.Political)).ContinueWith(
            //        continuationAction: x => this.PoliticalNews = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}

            //if (this.IsLoading = this.EducationNews.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.Education)).ContinueWith(
            //        continuationAction: x => this.EducationNews = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}

            //if (this.IsLoading = this.TechnologyNews.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.Technology)).ContinueWith(
            //        continuationAction: x => this.TechnologyNews = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}

            //if (this.IsLoading = this.GovernmentNews.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.Government)).ContinueWith(
            //        continuationAction: x => this.GovernmentNews = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}

            //if (this.IsLoading = this.ProvincialNews.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.ProvincialNews)).ContinueWith(
            //        continuationAction: x => this.ProvincialNews = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}

            //if (this.IsLoading = this.ScienceNews.IsNullOrEmpty())
            //{
            //    store.Get(new NewsMessage.Request(Category.ScienceAndEnvironment)).ContinueWith(
            //        continuationAction: x => this.ScienceNews = x.Result.AsItems(),
            //        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            //}
        }

        private void NavigateToNewsDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: newsItem));
        }
    }
}
