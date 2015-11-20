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
        }
    }
}
