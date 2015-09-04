using System.Linq;
using Newtonsoft.Json;
using ReactiveUI;
using UNTv.WP81.DataProviders.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.DataProviders.Contracts.Messages
{
    public class NewsResponse
    {
        [JsonProperty("status")]
        public virtual string Status { get; set; }

        [JsonProperty("count")]
        public virtual long Count { get; set; }

        [JsonProperty("pages")]
        public virtual long Pages { get; set; }

        [JsonProperty("category")]
        public virtual Category Category { get; set; }

        [JsonProperty("posts")]
        public virtual Post[] Posts { get; set; }

        public virtual ReactiveList<ItemViewModel> AsItems()
        {
            var items = this.Posts
                .Select(x => new ItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Subtitle = x.Date.HasValue
                        ? x.Date.Value.ToString("D")
                        : x.TitlePlain,
                    Category = this.Category.Title,
                    Description = x.Excerpt,
                    ImageUri = x.Attachments.Any()
                        ? x.Attachments.First().Images[ImageSize.Full].Uri
                        : "/Assets/LightGray.png",
                    Content = x.Content
                });

            return new ReactiveList<ItemViewModel>(items);
        }
    }

}
