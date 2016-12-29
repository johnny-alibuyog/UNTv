using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Data.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.News
{
    public class NewsHubViewModel : ReactiveRoutableBase
    {
        private readonly RoutingState _router;
        private readonly IDataService _service;

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
            _service = Locator.CurrentMutable.GetService<IDataService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate(x as Category));

            this.NavigateToNewsDetailCommand = ReactiveCommand.Create();
            this.NavigateToNewsDetailCommand.Subscribe(x => NavigateToNewsDetail((ItemViewModel)x));

            this.WhenAnyValue(x => x.CurrentSection)
                .Subscribe(x => this.PopulateCommand.Execute(x));

            // Setup progress bar
            this.WhenAnyValue(x => x.Headlines)
                .Where(x => this.CurrentSection == Category.Headlines)
                .Subscribe(x => this.IsLoading = x.IsNullOrEmpty());

            this.WhenAnyValue(x => x.WorldNews)
                .Where(x => this.CurrentSection == Category.World)
                .Subscribe(x => this.IsLoading = x.IsNullOrEmpty());

            this.WhenAnyValue(x => x.HealthNews)
                .Where(x => this.CurrentSection == Category.Health)
                .Subscribe(x => this.IsLoading = x.IsNullOrEmpty());

            this.WhenAnyValue(x => x.PoliticalNews)
                .Where(x => this.CurrentSection == Category.Political)
                .Subscribe(x => this.IsLoading = x.IsNullOrEmpty());

            this.WhenAnyValue(x => x.EducationNews)
                .Where(x => this.CurrentSection == Category.Education)
                .Subscribe(x => this.IsLoading = x.IsNullOrEmpty());

            this.WhenAnyValue(x => x.TechnologyNews)
                .Where(x => this.CurrentSection == Category.Technology)
                .Subscribe(x => this.IsLoading = x.IsNullOrEmpty());

            this.WhenAnyValue(x => x.GovernmentNews)
                .Where(x => this.CurrentSection == Category.Government)
                .Subscribe(x => this.IsLoading = x.IsNullOrEmpty());

            this.WhenAnyValue(x => x.ProvincialNews)
                .Where(x => this.CurrentSection == Category.ProvincialNews)
                .Subscribe(x => this.IsLoading = x.IsNullOrEmpty());

            this.WhenAnyValue(x => x.ScienceNews)
                .Where(x => this.CurrentSection == Category.ScienceAndEnvironment)
                .Subscribe(x => this.IsLoading = x.IsNullOrEmpty());

            // populate section headlines
            this.CurrentSection = Category.Headlines;
        }

        private void Populate(Category section = null)
        {
            Action<Category, ReactiveList<ItemViewModel>, Action<ReactiveList<ItemViewModel>>> PopulateCurrentSectionIfEmpty = (category, items, callback) =>
            {
                var isCurrentSectionEmpty = (section == category) && (this.IsLoading = items.IsNullOrEmpty());
                if (isCurrentSectionEmpty)
                {
                    _service.Get(new NewsMessage.Request(category)).ContinueWith(
                        continuationAction: x => callback(x.Result.AsItems()),
                        scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                    );
                }
            };

            PopulateCurrentSectionIfEmpty(Category.Headlines, this.Headlines, result => this.Headlines = result);
            PopulateCurrentSectionIfEmpty(Category.World, this.WorldNews, result => this.WorldNews = result);
            PopulateCurrentSectionIfEmpty(Category.Sports, this.SportsNews, result => this.SportsNews = result);
            PopulateCurrentSectionIfEmpty(Category.Health, this.HealthNews, result => this.HealthNews = result);
            PopulateCurrentSectionIfEmpty(Category.Political, this.PoliticalNews, result => this.PoliticalNews = result);
            PopulateCurrentSectionIfEmpty(Category.Education, this.EducationNews, result => this.EducationNews = result);
            PopulateCurrentSectionIfEmpty(Category.Technology, this.TechnologyNews, result => this.TechnologyNews = result);
            PopulateCurrentSectionIfEmpty(Category.Government, this.GovernmentNews, result => this.GovernmentNews = result);
            PopulateCurrentSectionIfEmpty(Category.ProvincialNews, this.ProvincialNews, result => this.ProvincialNews = result);
            PopulateCurrentSectionIfEmpty(Category.ScienceAndEnvironment, this.ScienceNews, result => this.ScienceNews = result);
        }

        private void NavigateToNewsDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: newsItem));
        }
    }
}
