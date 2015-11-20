using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Data.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Data.Contracts.Messages
{
    public static class NewsMessage
    {
        public class Request : IReturn<Response>
        {
            public virtual Nullable<int> Count { get; set; }

            public virtual Category Category { get; set; }

            public Request() { }

            public Request(Category category = null, Nullable<int> count = null)
            {
                this.Count = count;
                this.Category = category ?? Category.Headlines;
            }
        }

        public class Response
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

            public Response()
            {
                this.Posts = new Post[] { };
            }

            public virtual ReactiveList<ItemViewModel> AsItems()
            {
                Func<IDictionary<string, Image>, string> GetImageUri = (images) =>
                {
                    if (images.Count == 0)
                        return "/Assets/Images/LightGray.png";

                    if (images.ContainsKey(ImageSize.Full))
                        return images[ImageSize.Full].Uri;

                    if (images.ContainsKey(ImageSize.Large))
                        return images[ImageSize.Large].Uri;

                    if (images.ContainsKey(ImageSize.Medium))
                        return images[ImageSize.Medium].Uri;

                    if (images.ContainsKey(ImageSize.Small))
                        return images[ImageSize.Small].Uri;

                    if (images.ContainsKey(ImageSize.Thumbnail))
                        return images[ImageSize.Thumbnail].Uri;

                    if (images.ContainsKey(ImageSize.Portfolio))
                        return images[ImageSize.Portfolio].Uri;

                    if (images.ContainsKey(ImageSize.PageReview))
                        return images[ImageSize.PageReview].Uri;

                    if (images.ContainsKey(ImageSize.ProgramReview))
                        return images[ImageSize.ProgramReview].Uri;

                    if (images.ContainsKey(ImageSize.ProgramReviewFeatured))
                        return images[ImageSize.ProgramReviewFeatured].Uri;

                    if (images.ContainsKey(ImageSize.WPTouchNewThumbnail))
                        return images[ImageSize.WPTouchNewThumbnail].Uri;

                    if (images.ContainsKey(ImageSize.FoundationFeaturedImage))
                        return images[ImageSize.FoundationFeaturedImage].Uri;

                    return images.First().Value.Uri;
                };

                if (this.Posts == null || this.Posts.Count() == 0)
                    return null;

                var items = this.Posts
                    .Select(x => new ItemViewModel()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Subtitle = x.Date.HasValue
                            ? x.Date.Value.GetRelativeTime()
                            : x.TitlePlain,
                        Category = this.Category.Title,
                        Description = x.Excerpt,
                        ImageUri = GetImageUri(!x.Attachments.IsNullOrEmpty()
                            ? x.Attachments.First().Images
                            : x.ThumbnailImages
                        ),
                        Content = x.Content
                    });

                return new ReactiveList<ItemViewModel>(items);
            }
        }
    }
}
