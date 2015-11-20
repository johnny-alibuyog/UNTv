using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.Data.Entities
{
    public class Forecast
    {
        [JsonProperty("period")]
        public virtual long Id { get; set; }

        [JsonProperty("icon_url")]
        public virtual string ImageUri { get; set; }

        [JsonProperty("title")]
        public virtual string Period { get; set; }

        [JsonProperty("fcttext_metric")]
        public virtual string Details { get; set; }
    }
}
