using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UNTv.WP81.Data.Contracts.Messages
{
    public enum StreamingMedia
    {
        Audio,
        Video
    }

    public class StreamingMessage
    {
        public class Request : IReturn<Response> 
        {
            public virtual StreamingMedia Media { get; set; }

            public Request(StreamingMedia media)
            {
                this.Media = media;
            }
        }

        public class Response
        {
            public virtual Uri Uri { get; set; }

            [JsonProperty("posts")]
            private JArray RawData { get; set; }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context)
            {
                this.Uri = new Uri((string)RawData[0]["excerpt"]);
            }
        }
    }
}
