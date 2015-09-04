using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UNTv.WP81.DataProviders.Entities
{
    public class RadioProgram
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

        [JsonProperty("programdescription_program_banner_for_mobile")]
        public virtual string BannerUri { get; set; }

        [JsonProperty("programdescription_program_thumbnail_mobile")]
        public virtual string BannerThumbnailUri { get; set; }

        public virtual Host[] Hosts { get; set; }

        [JsonProperty("hosts")]
        private JArray RawHosts { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.Hosts = RawHosts
                .Select(x => new Host()
                {
                    Name = x["host"].ToString(),
                    ImageUri = x["host_thumbnail"].ToString()
                })
                .ToArray();
        }
    }
}
