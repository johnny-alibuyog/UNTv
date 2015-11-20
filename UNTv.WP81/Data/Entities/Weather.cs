using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.Data.Entities
{
    public class Weather
    {
        [JsonProperty("location")]
        public virtual Location Location { get; set; }

        public virtual Forecast[] Forcasts { get; set; }
    }
}
