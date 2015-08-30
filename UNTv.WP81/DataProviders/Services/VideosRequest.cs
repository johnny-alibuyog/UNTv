using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.DataProviders.Services
{
    public enum SortFilter
    {
        Latest,
        Featured,
        Popular
    }

    public class VideosRequest
    {
        public virtual SortFilter SortBy { get; set; }

        public VideosRequest() { }

        public VideosRequest(SortFilter sortBy)
        {
            this.SortBy = sortBy;
        }
    }
}
