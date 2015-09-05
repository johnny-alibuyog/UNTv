using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.Data.Entities
{
    public class ImageSize
    {
        public static readonly string Full = "full";

        public static readonly string Thumbnail = "thumbnail";

        public static readonly string Small = "small";

        public static readonly string Medium = "medium";

        public static readonly string Large = "large";

        public static readonly string Portfolio = "portfolio";

        public static readonly string PageReview = "page-preview";

        public static readonly string ProgramReview = "program-preview";

        public static readonly string ProgramReviewFeatured = "program-preview-featured";

        public static readonly string WPTouchNewThumbnail = "wptouch-new-thumbnail";

        public static readonly string FoundationFeaturedImage = "foundation-featured-image";
    }
}
