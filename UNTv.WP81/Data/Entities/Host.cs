using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.Data.Entities
{
    public class Host
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("title")]
        public virtual string Name { get; set; }

        [JsonProperty("description")]
        public virtual string Description { get; set; }

        [JsonProperty("teaser_video")]
        public virtual string VideoUri { get; set; }

        [JsonProperty("banner_image")]
        public virtual string ImageUri { get; set; }

        [JsonProperty("teaser_poster")]
        public virtual string PosterUri { get; set; }

        [JsonProperty("host_banner")]
        public virtual string BannerUri { get; set; }

        [JsonProperty("thumbnail")]
        public virtual string ThumbnailUri { get; set; }
    }
}
