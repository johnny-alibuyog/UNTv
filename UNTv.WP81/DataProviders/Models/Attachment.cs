using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.DataProviders.Models
{
    public class Attachment
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("url")]
        public virtual string Url { get; set; }

        [JsonProperty("slug")]
        public virtual string Slug { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("description")]
        public virtual string Description { get; set; }

        [JsonProperty("caption")]
        public virtual string Caption { get; set; }

        [JsonProperty("parent")]
        public virtual long Parent { get; set; }

        [JsonProperty("mime_type")]
        public virtual string MimeType { get; set; }

        [JsonProperty("images")]
        public virtual Dictionary<string, Image> Images { get; set; }
    }
}
