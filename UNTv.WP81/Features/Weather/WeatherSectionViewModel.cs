using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Data.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;
using Windows.Devices.Geolocation;

namespace UNTv.WP81.Features.Weather
{
    public class WeatherSectionViewModel : ReactiveBase
    {
        private readonly RoutingState _router;
        private readonly IDataService _service;

        public virtual Location Location { get; set; }
        public virtual ReactiveList<ItemViewModel> Forecast { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }

        public WeatherSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IDataService>();

            this.PopulateCommand = ReactiveCommand.Create(this.WhenAny(x => x.Location, x => x.Value != null));
            this.PopulateCommand.Subscribe(x => Populate());
        }

        private void Populate()
        {
            if (this.Forecast.IsNullOrEmpty())
            {
                _service.Get(new WeatherMessage.Request(Location.Longitude, Location.Latitude)).ContinueWith(
                    continuationAction: x => this.Forecast = x.Result.AsItems(),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }

    }
}
