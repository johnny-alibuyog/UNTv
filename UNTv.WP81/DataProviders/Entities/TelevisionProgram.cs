using Newtonsoft.Json;

namespace UNTv.WP81.DataProviders.Entities
{
    public class TelevisionProgram
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("description")]
        public virtual string Description { get; set; }

        [JsonProperty("schedule")]
        public virtual string Schedule { get; set; }

        [JsonProperty("thumbnail")]
        public virtual string ThumbnailUri { get; set; }

        [JsonProperty("program_banner")]
        public virtual string BannerUri { get; set; }

        [JsonProperty("program_banner_thumbnail")]
        public virtual string BannerThumbnailUri { get; set; }
    }
}
