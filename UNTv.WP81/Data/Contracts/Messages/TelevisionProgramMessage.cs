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
    public static class TelevisionProgramMessage {

        public class Request : IReturn<Response> { }

        public class Response
        {
            [JsonProperty("status")]
            public virtual string Status { get; set; }

            public virtual TelevisionProgram[] Programs { get; set; }

            [JsonProperty("programs")]
            private JArray RawData { get; set; }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context)
            {
                this.Programs = JsonConvert.DeserializeObject<TelevisionProgram[]>(RawData[0].ToString());
            }

            public Response()
            {
                this.Programs = new TelevisionProgram[] { };
            }

            public virtual ReactiveList<ItemViewModel> AsItems()
            {
                Predicate<string> ValidateUri = (stringUri) =>
                {
                    var outUri = (Uri)null;
                    return Uri.TryCreate(stringUri, UriKind.Absolute, out outUri);
                };

                Func<string, string> CreateImageTag = (uri) => string.Format("<img src=\"{0}\" \\> <br \\>", uri);

                Func<TelevisionProgram, string> ChoseFromImages = (x) =>
                    ValidateUri(x.BannerUri) ? x.BannerUri :
                    ValidateUri(x.ThumbnailUri) ? x.ThumbnailUri :
                    ValidateUri(x.BannerThumbnailUri) ? x.BannerThumbnailUri : "/Assets/LightGray.png";

                var items = this.Programs
                 .Select(x => new ItemViewModel()
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Subtitle = x.Schedule,
                     Category = "Television Programs",
                     ImageUri = ChoseFromImages(x),
                     Content = CreateImageTag(ChoseFromImages(x)) + x.Description
                 });

                return new ReactiveList<ItemViewModel>(items);
            }
        }
    }
}
