using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.DataProviders.Entities
{
    public class Tag
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("slug")]
        public virtual string Slug { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("description")]
        public virtual string Description { get; set; }

        [JsonProperty("post_count")]
        public virtual long PostCount { get; set; }
    }
}
