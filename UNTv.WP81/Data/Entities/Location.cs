using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.Data.Entities
{
    public class Location
    {
        [JsonProperty("country_name")]
        public virtual string Country { get; set; }

        [JsonProperty("city")]
        public virtual string City { get; set; }

        [JsonProperty("state")]
        public virtual string State { get; set; }

        public virtual DateTimeOffset Timestamp { get; set; }

        [JsonProperty("tz_long")]
        public virtual string TimeZone { get; set; }

        [JsonProperty("lon")]
        public virtual double Longitude { get; set; }

        [JsonProperty("lat")]
        public virtual double Latitude { get; set; }
    }
}
