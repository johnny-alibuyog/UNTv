using ReactiveUI;
using Splat;
using System;
using System.Threading.Tasks;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Data.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.News
{
    public class NewsSectionViewModel : ReactiveBase
    {
        private readonly RoutingState _router;
        private readonly IDataService _service;

        public virtual ReactiveList<ItemViewModel> News { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToNewsHubCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToNewsDetailCommand { get; private set; }

        public NewsSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IDataService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToNewsHubCommand = ReactiveCommand.Create();
            this.NavigateToNewsHubCommand.Subscribe(x => NavigateToNewsHub());

            this.NavigateToNewsDetailCommand = ReactiveCommand.Create();
            this.NavigateToNewsDetailCommand.Subscribe(x => NavigateToNewsDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            if (this.News.IsNullOrEmpty())
            {
                _service.Get(new NewsMessage.Request(Category.Headlines)).ContinueWith(
                    continuationAction: x => this.News = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }

        private void NavigateToNewsHub()
        {
            _router.Navigate.Execute(Locator.CurrentMutable.GetService<NewsHubViewModel>());
        }

        private void NavigateToNewsDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: newsItem));
        }
    }
}
