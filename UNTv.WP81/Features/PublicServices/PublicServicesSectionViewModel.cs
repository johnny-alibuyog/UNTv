using System;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.DataProviders.Contracts.Messages;
using UNTv.WP81.DataProviders.Contracts.Services;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.PublicServices
{
    public class PublicServicesSectionViewModel: ReactiveBase
    {
        private readonly RoutingState _router;
        private readonly ITelevisionService _service;

        public virtual ReactiveList<ItemViewModel> Programs { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToProgramsDetailCommand { get; private set; }

        public PublicServicesSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<ITelevisionService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToProgramsDetailCommand = ReactiveCommand.Create();
            this.NavigateToProgramsDetailCommand.Subscribe(x => NavigateToNewsDetail((ItemViewModel)x));
        }

        private void Populate()
        {
            _service.Get(new PublicServicesRequest()).ContinueWith(
                continuationAction: x => this.Programs = x.Result.AsItems(),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void NavigateToNewsDetail(ItemViewModel newsItem)
        {
            _router.Navigate.Execute(new DetailViewModel(value: newsItem));
        }
    }
}
