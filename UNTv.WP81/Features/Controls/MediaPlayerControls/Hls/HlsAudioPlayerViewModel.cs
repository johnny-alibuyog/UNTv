using ReactiveUI;
using System;

namespace UNTv.WP81.Features.Controls.MediaPlayerControls.Hls
{
    public class HlsAudioPlayerViewModel : ReactiveRoutableBase
    {
        public virtual Uri MediaUri { get; set; }

        public HlsAudioPlayerViewModel(IScreen hostScreen = null, Uri mediaUri = null)
            : base(hostScreen)
        {
            this.MediaUri = mediaUri;
        }
    }
}
