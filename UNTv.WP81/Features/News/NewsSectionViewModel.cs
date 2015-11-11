using System;
using System.Net.NetworkInformation;
using System.Threading;
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
    public class NewsSectionViewModel : ReactiveBase
    {
        private readonly IStore _webStore;
        private readonly IStore _localStore;
        private readonly RoutingState _router;

        public virtual ReactiveList<ItemViewModel> News { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToNewsHubCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToNewsDetailCommand { get; private set; }

        public NewsSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _webStore = Locator.CurrentMutable.GetService<WebStore>();
            _localStore = Locator.CurrentMutable.GetService<LocalStore>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToNewsHubCommand = ReactiveCommand.Create();
            this.NavigateToNewsHubCommand.Subscribe(x => NavigateToNewsHub());

            this.NavigateToNewsDetailCommand = ReactiveCommand.Create();
            this.NavigateToNewsDetailCommand.Subscribe(x => NavigateToNewsDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            //Task.Factory.StartNew(() => Populate(_localStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            //if (!NetworkInterface.GetIsNetworkAvailable())
            //    return;

            //Task.Factory.StartNew(() => Populate(_webStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            Populate(_webStore);
        }

        private void Populate(IStore store)
        {
            if (this.News.IsNullOrEmpty())
            {
                store.Get(new NewsMessage.Request(Category.Headlines)).ContinueWith(
                    continuationAction: x => this.News = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }

        private void NavigateToNewsHub()
        {
            _router.Navigate.Execute(new NewsHubViewModel());
        }

        private void NavigateToNewsDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: newsItem));
        }
    }
}
