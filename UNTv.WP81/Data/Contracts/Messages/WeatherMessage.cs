using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using UNTv.WP81.Data.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;
using UNTv.WP81.Features.Weather;

namespace UNTv.WP81.Data.Contracts.Messages
{
    public class WeatherMessage
    {
        public class Request : IReturn<Response>
        {
            public virtual double Longitude { get; set; }

            public virtual double Latitude { get; set; }

            public Request(double longitude, double latitude)
            {
                this.Longitude = longitude;
                this.Latitude = latitude;
            }
        }

        public class Response
        {
            [JsonProperty("location")]
            public virtual Location Location { get; set; }

            public virtual Forecast[] Forecast { get; set; }

            [JsonProperty("forecast")]
            public virtual JObject RawForcast { get; set; }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context)
            {
                var forcastString = RawForcast["txt_forecast"]["forecastday"].ToString();
                this.Forecast = JsonConvert.DeserializeObject<Forecast[]>(forcastString);
            }

            public ReactiveList<ItemViewModel> AsItems()
            {
                if (this.Forecast == null || this.Forecast.Count() == 0)
                    return null;

                var items = this.Forecast
                 .Select(x => new ItemViewModel()
                 {
                     Id = x.Id,
                     Title = x.Details,
                     Subtitle = x.Period,
                     Category = "Forecast",
                     ImageUri = x.ImageUri
                 });

                return new ReactiveList<ItemViewModel>(items);
                
            }

            public ReactiveList<ForecastItemViewModel> AsForcastItems()
            {
                if (this.Forecast == null || this.Forecast.Count() == 0)
                    return null;

                var items = this.Forecast
                    .GroupBy(x => x.Period.Split(' ').First())
                    .Select(x => new ForecastItemViewModel()
                    {
                        Title = new string(x.Key.Take(3).ToArray()),
                        Day = new ForecastItemDetailViewModel()
                        {
                            Name = x.First().Period,
                            Details = x.First().Details,
                            ImageUri = x.First().ImageUri,
                        },
                        Night = new ForecastItemDetailViewModel()
                        {
                            Name = x.Last().Period,
                            Details = x.Last().Details,
                            ImageUri = x.Last().ImageUri,
                        }
                    })
                    .ToList();

                return new ReactiveList<ForecastItemViewModel>(items);

            }

        }
    }
}
