﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UNTv.WP81.DataProviders.Entities
{
    public class RadioProgramSchedule
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
