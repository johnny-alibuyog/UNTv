using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.Data.Entities
{
    public class Image
    {
        [JsonProperty("url")]
        public virtual string Uri { get; set; }

        [JsonProperty("width")]
        public virtual long Width { get; set; }

        [JsonProperty("height")]
        public virtual long Height { get; set; }
    }   
}
