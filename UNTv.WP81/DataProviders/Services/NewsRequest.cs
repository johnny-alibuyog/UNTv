using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNTv.WP81.DataProviders.Models;

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
}
