using System;

namespace UNTv.WP81.DataProviders.Contracts.Messages
{
    public enum SortFilter
    {
        Latest,
        Featured,
        Popular
    }

    public class VideosRequest
    {
        public virtual Nullable<int> Count { get; set; }

        public virtual SortFilter SortBy { get; set; }

        public VideosRequest() { }

        public VideosRequest(SortFilter sortBy)
        {
            this.SortBy = sortBy;
        }
    }
}
