using System;
using ReactiveUI;

namespace UNTv.WP81.Features.Controls
{
    public class HlsPlayerViewModel: ReactiveRoutableBase
    {
        public virtual Uri VideoUri { get; set; }

        public HlsPlayerViewModel(IScreen hostScreen = null) : base(hostScreen) { }
    }
}
