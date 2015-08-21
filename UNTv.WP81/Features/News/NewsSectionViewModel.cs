using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.DataProviders.Models;
using UNTv.WP81.DataProviders.Services;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.News
{
    public class NewsSectionViewModel : ReactiveBase
    {
        private RoutingState _router;
        private IWebTvService _service;

        public ReactiveList<ItemViewModel> Headlines { get; set; }

        public ReactiveCommand<Object> InitializeCommand { get; private set; }

        public ReactiveCommand<Object> NavigateToNewsPageCommand { get; private set; }

        public ReactiveCommand<Object> NavigateToNewsDetailCommand { get; private set; }

        public NewsSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IWebTvService>();

            this.NavigateToNewsPageCommand = ReactiveCommand.Create();
            this.NavigateToNewsPageCommand.Subscribe(x => NavigateToNewsPage());

            this.NavigateToNewsDetailCommand = ReactiveCommand.Create();
            this.NavigateToNewsDetailCommand.Subscribe(x => NavigateToNewsDetail((ItemViewModel)x));

            Populate();
        }

        private void Populate()
        {
            _service.Get(new NewsRequest(Category.Headlines)).ContinueWith(
                continuationAction: x => this.Headlines = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void NavigateToNewsPage()
        {
            _router.Navigate.Execute(new NewsPageViewModel());
        }

        private void NavigateToNewsDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: newsItem));
        }
    }
}
