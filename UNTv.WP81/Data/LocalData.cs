using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNTv.WP81.Data.Entities;

namespace UNTv.WP81.Data
{
    // TODO: For performance and offline usage, we will encorporate local data store soon
    public static class LocalData
    {
        private static object newsLock = new object();
        private static IDictionary<Category, Post[]> _news = new Dictionary<Category, Post[]>();

        public static IDictionary<Category, Post[]> News
        {
            get  { lock(newsLock) return _news; }
        }
    }
}
