using Newtonsoft.Json;

namespace UNTv.WP81.DataProviders.Models
{
    public class TelevisionProgramSchedule
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("description")]
        public virtual string Description { get; set; }

        [JsonProperty("day")]
        public virtual string Day { get; set; }

        [JsonProperty("time_start")]
        public virtual string StartTime { get; set; }

        [JsonProperty("time_end")]
        public virtual string EndTime { get; set; }

        [JsonProperty("thumbnail")]
        public virtual string ThumbnailUri { get; set; }

        [JsonProperty("program_banner")]
        public virtual string BannerUri { get; set; }

        [JsonProperty("program_banner_thumbnail")]
        public virtual string BannerThumbnailUri { get; set; }
    }
}
