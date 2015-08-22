using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using UNTv.WP81.DataProviders.Models;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.DataProviders.Services
{

    public class NewsRequest
    {
        public virtual Category Category { get; set; }

        public NewsRequest() { }

        public NewsRequest(Category category)
        {
            this.Category = category;
        }
    }

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
                        : "Assets/LightGray.png",
                    Content = x.Content
                });

            return new ReactiveList<ItemViewModel>(items);
        }
    }

    public class TvProgramRequest
    {
        public virtual Filter SortBy { get; set; }

        public TvProgramRequest() { }

        public TvProgramRequest(Filter sortBy)
        {
            this.SortBy = sortBy;
        }

        public enum Filter
        {
            Latest,
            Featured,
            Popular
        }

    }

    public class TvProgramResponse
    {
        [JsonProperty("status")]
        public virtual string Status { get; set; }

        public virtual long Page { get; set; }

        public virtual long TotalPage { get; set; }

        public virtual long TotalCount { get; set; }

        public virtual TvProgram[] Programs { get; set; }

        [JsonProperty("programs")]
        private JArray RawData { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.Page = (long)RawData[1]["page"];
            this.TotalPage = (long)RawData[3]["total_page"];
            this.TotalCount = (long)RawData[2]["total_count"];
            this.Programs = JsonConvert.DeserializeObject<TvProgram[]>(RawData[0].ToString());
        }

        public virtual ReactiveList<ItemViewModel> AsItems()
        {
            var items = this.Programs
             .Select(x => new ItemViewModel()
             {
                 Id = x.Id,
                 Title = x.Title,
                 Subtitle = x.ProgramTitle,
                 Category = "TV Programs",
                 ImageUri = x.ImageUri,
                 Content = x.YoutubeVideoId
             });

            return new ReactiveList<ItemViewModel>(items);
        }
    }

    public interface IWebTvService
    {
        Task<NewsResponse> Get(NewsRequest request);
        Task<TvProgramResponse> Get(TvProgramRequest request);
    }
}
