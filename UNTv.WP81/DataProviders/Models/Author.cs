using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.DataProviders.Models
{
    public class Author
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("slug")]
        public virtual string Slug { get; set; }

        [JsonProperty("name")]
        public virtual string Name { get; set; }

        [JsonProperty("first_name")]
        public virtual string FirstName { get; set; }

        [JsonProperty("last_name")]
        public virtual string LastName { get; set; }

        [JsonProperty("nickname")]
        public virtual string Nickname { get; set; }

        [JsonProperty("url")]
        public virtual string Url { get; set; }

        [JsonProperty("description")]
        public virtual string Description { get; set; }
    }
}
