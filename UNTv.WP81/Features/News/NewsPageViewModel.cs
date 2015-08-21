using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Features;

namespace UNTv.WP81.Features.News
{
    public class NewsPageViewModel : ReactiveRoutableBase
    {
        public NewsPageViewModel(IScreen hostScreen = null) : base(hostScreen) { }
    }
}
