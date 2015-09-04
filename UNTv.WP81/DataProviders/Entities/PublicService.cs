using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UNTv.WP81.DataProviders.Entities
{
    public class PublicService
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("description")]
        public virtual string Description { get; set; }

        [JsonProperty("page_banner")]
        public virtual string PageBannerUri { get; set; }

        [JsonProperty("thumbnail")]
        public virtual string ThumbnailUri { get; set; }

        public virtual IDictionary<string, string> Images { get; set; }

        [JsonProperty("images")]
        private JArray RawData { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.Images = JsonConvert.DeserializeObject<IDictionary<string, string>>(RawData[0].ToString());
        }
    }
}
