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
    public enum SortFilter
    {
        Latest,
        Featured,
        Popular
    }

    public static class VideoMessage
    {
        public class Request : IReturn<Response>
        {
            public virtual Nullable<int> Count { get; set; }

            public virtual SortFilter SortBy { get; set; }

            public Request() { }

            public Request(SortFilter sortBy)
            {
                this.SortBy = sortBy;
            }
        }

        public class Response
        {
            [JsonProperty("status")]
            public virtual string Status { get; set; }

            public virtual long Page { get; set; }

            public virtual long TotalPage { get; set; }

            public virtual long TotalCount { get; set; }

            public virtual Video[] Videos { get; set; }

            [JsonProperty("programs")]
            private JArray RawData { get; set; }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context)
            {
                this.Page = (long)RawData[1]["page"];
                this.TotalPage = (long)RawData[3]["total_page"];
                this.TotalCount = (long)RawData[2]["total_count"];
                this.Videos = JsonConvert.DeserializeObject<Video[]>(RawData[0].ToString());
            }

            public Response()
            {
                this.Videos = new Video[] { };
            }

            public virtual ReactiveList<ItemViewModel> AsItems()
            {
                if (this.Videos == null || this.Videos.Count() == 0)
                    return null;

                var items = this.Videos
                 .Select(x => new ItemViewModel()
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Subtitle = x.ProgramTitle,
                     Category = "Videos",
                     ImageUri = !string.IsNullOrWhiteSpace(x.ImageUri)
                        ? x.ImageUri : "/Assets/Images/LightGray.png",
                     Content = x.YoutubeVideoId
                 });

                return new ReactiveList<ItemViewModel>(items);
            }
        }
    }
}
