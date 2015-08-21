using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNTv.WP81.Features;

namespace UNTv.WP81.Features.News
{
    public class NewsViewModel : ReactiveBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
