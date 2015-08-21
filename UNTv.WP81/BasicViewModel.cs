using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;

namespace UNTv.WP81
{
    public class BasicViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment { get; protected set; }

        public BasicViewModel(IScreen hostScreen = null)
        {
            this.HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            this.UrlPathSegment = "/basic";
        }
    }
}
