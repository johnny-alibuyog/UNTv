using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using UNTv.WP81.DataProviders.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.DataProviders.Contracts.Messages
{
    public class RadioProgramsResponse
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

        public virtual ReactiveList<ItemViewModel> AsItems()
        {
            Predicate<string> ValidateUri = (stringUri) =>
            {
                var outUri = (Uri)null;
                return Uri.TryCreate(stringUri, UriKind.Absolute, out outUri);
            };

            Func<string, string> CreateImageTag = (uri) => string.Format("<img src=\"{0}\" \\> <br \\>", uri);

            Func<RadioProgram, string> ChoseFromImages = (x) =>
                ValidateUri(x.BannerThumbnailUri) ? x.BannerThumbnailUri :
                ValidateUri(x.ThumbnailUri) ? x.ThumbnailUri :
                ValidateUri(x.BannerUri) ? x.BannerUri : "/Assets/LightGray.png";

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
