using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using UNTv.WP81.Data.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Data.Contracts.Messages
{
    public static class PublicServiceMessage {

        public class Request : IReturn<Response> { }

        public class Response
        {
            [JsonProperty("status")]
            public virtual string Status { get; set; }

            public virtual PublicService[] Programs { get; set; }

            [JsonProperty("programs")]
            private JArray RawData { get; set; }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context)
            {
                this.Programs = JsonConvert.DeserializeObject<PublicService[]>(RawData[0].ToString());
            }

            public Response()
            {
                this.Programs = new PublicService[] { };
            }

            public virtual ReactiveList<ItemViewModel> AsItems()
            {
                Predicate<string> ValidateUri = (stringUri) =>
                {
                    var outUri = (Uri)null;
                    return Uri.TryCreate(stringUri, UriKind.Absolute, out outUri);
                };

                Func<string, string> CreateImageTag = (uri) => string.Format("<img src=\"{0}\" height=\"132\" width=\"300\" \\> <br \\>", uri);

                var items = this.Programs
                 .Select(x => new ItemViewModel()
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Subtitle = string.Empty,
                     Category = "Public Service",
                     ImageUri = x.Images[ImageSize.Medium],
                     Content = CreateImageTag(x.Images[ImageSize.Medium]) + x.Description
                 })
                 .ToList();

                return new ReactiveList<ItemViewModel>(items);
            }
        }
    }
}
