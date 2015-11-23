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

        public virtual LocationViewModel Location { get; set; }
        public virtual ReactiveList<ForecastItemViewModel> Forecast { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }

        public WeatherSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IDataService>();

            this.Location = new LocationViewModel();

            this.PopulateCommand = ReactiveCommand.Create(this.WhenAny(
                property1: x => x.Location.Longitude,
                property2: x => x.Location.Latitude, 
                selector: (longitude, latitude) => longitude.Value > 0 && latitude.Value > 0
            ));
            this.PopulateCommand.Subscribe(x => Populate());
        }

        private void Populate()
        {
            Action<WeatherMessage.Response> Populate = (response) =>
            {
                this.Location.Place = string.Format("{0}, {1}", response.Location.City, response.Location.Country);
                this.Location.Timestamp = DateTimeOffset.Now; //response.Location.Timestamp;
                this.Location.TimeZone = response.Location.TimeZone;
                this.Location.Longitude = response.Location.Longitude;
                this.Location.Latitude = response.Location.Latitude;
                this.Forecast = response.AsForcastItems();

            };

            if (this.Forecast.IsNullOrEmpty())
            {
                _service.Get(new WeatherMessage.Request(Location.Longitude, Location.Latitude)).ContinueWith(
                    continuationAction: x => Populate(x.Result),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }
    }
}
