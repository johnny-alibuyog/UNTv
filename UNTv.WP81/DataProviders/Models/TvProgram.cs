﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.DataProviders.Models
{
    public class TvProgram
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("program_id")]
        public virtual long ProgramId { get; set; }

        [JsonProperty("program_title")]
        public virtual string ProgramTitle { get; set; }

        [JsonProperty("thumbnail")]
        public virtual string ImageUri { get; set; }

        [JsonProperty("url")]
        public virtual string VideoUri { get; set; }

        [JsonProperty("video_id")]
        public virtual string YoutubeVideoId { get; set; }

    }
}
