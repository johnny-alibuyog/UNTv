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
    public static class RadioProgramMessage {

        public class Request : IReturn<Response> { }

        public class Response
        {
            [JsonProperty("status")]
            public virtual string Status { get; set; }

            public virtual RadioProgram[] Programs { get; set; }

            [JsonProperty("programs")]
            private JArray RawData { get; set; }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context)
            {
                this.Programs = JsonConvert.DeserializeObject<RadioProgram[]>(RawData[0].ToString());
            }

            public Response()
            {
                this.Programs = new RadioProgram[] { };
            }

            public virtual ReactiveList<ItemViewModel> AsItems()
            {
                Predicate<string> ValidateUri = (stringUri) =>
                {
                    if (string.IsNullOrWhiteSpace(stringUri))
                        return false;

                    var outUri = (Uri)null;
                    return Uri.TryCreate(stringUri, UriKind.Absolute, out outUri);
                };

                Func<string, string> CreateImageTag = (uri) => string.Format("<img src=\"{0}\" \\> <br \\>", uri);

                Func<RadioProgram, string> ChoseFromImages = (x) =>
                    ValidateUri(x.BannerThumbnailUri) ? x.BannerThumbnailUri :
                    ValidateUri(x.ThumbnailUri) ? x.ThumbnailUri :
                    ValidateUri(x.BannerUri) ? x.BannerUri : "/Assets/Images/LightGray.png";

                if (this.Programs == null || this.Programs.Count() == 0)
                    return null;

                var items = this.Programs
                 .Select(x => new ItemViewModel()
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Subtitle = x.Schedule,
                     Category = "Radio Programs",
                     ImageUri = ChoseFromImages(x),
                     Content = CreateImageTag(ChoseFromImages(x)) + x.Description
                 });

                return new ReactiveList<ItemViewModel>(items);
            }
        }
    }
}
