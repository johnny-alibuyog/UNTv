using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.DataProviders.Models
{
    public class Post
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }

        [JsonProperty("type")]
        public virtual string Type { get; set; }

        [JsonProperty("slug")]
        public virtual string Slug { get; set; }

        [JsonProperty("url")]
        public virtual string Url { get; set; }

        [JsonProperty("status")]
        public virtual string Status { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("title_plain")]
        public virtual string TitlePlain { get; set; }

        [JsonProperty("content")]
        public virtual string Content { get; set; }

        [JsonProperty("excerpt")]
        public virtual string Excerpt { get; set; }

        [JsonProperty("date")]
        public virtual Nullable<DateTime> Date { get; set; }

        [JsonProperty("modified")]
        public virtual Nullable<DateTime> ModifiedOn { get; set; }

        [JsonProperty("categories")]
        public virtual Category[] Categories { get; set; }

        [JsonProperty("tags")]
        public virtual Tag[] Tags { get; set; }

        [JsonProperty("author")]
        public virtual Author Author { get; set; }

        [JsonProperty("comments")]
        public virtual Comment[] Comments { get; set; }

        [JsonProperty("attachments")]
        public virtual Attachment[] Attachments { get; set; }

        [JsonProperty("comment_count")]
        public virtual long CommentCount { get; set; }

        [JsonProperty("comment_status")]
        public virtual string CommentStatus { get; set; }

        [JsonProperty("custom_fields")]
        public virtual Dictionary<string, string[]> CustomFields { get; set; }

        [JsonProperty("thumbnail_size")]
        public virtual string ThumbnailSize { get; set; }

        [JsonProperty("thumbnail")]
        public virtual string ThumbnailUri { get; set; }

        [JsonProperty("thumbnail_images")]
        public virtual Dictionary<string, Image> ThumbnailImages { get; set; }
    }
}
