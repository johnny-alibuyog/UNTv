using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNTv.WP81.DataProviders.Entities;

namespace UNTv.WP81.DataProviders.Contracts.Messages
{
    public class NewsRequest
    {
        public virtual Nullable<int> Count { get; set; }

        public virtual Category Category { get; set; }

        public NewsRequest() { }

        public NewsRequest(Category category)
        {
            this.Category = category;
        }
    }
}
