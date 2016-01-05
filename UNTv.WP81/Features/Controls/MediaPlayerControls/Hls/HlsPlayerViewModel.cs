using System;
using ReactiveUI;

namespace UNTv.WP81.Features.Controls.MediaPlayerControls.Hls
{
    public class HlsPlayerViewModel : ReactiveRoutableBase
    {
        public virtual Uri MediaUri { get; set; }

        public HlsPlayerViewModel(IScreen hostScreen = null, Uri mediaUri = null)
            : base(hostScreen)
        {
            this.MediaUri = mediaUri;
        }
    }
}
