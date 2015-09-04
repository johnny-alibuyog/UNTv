﻿using System;
using ReactiveUI;

namespace UNTv.WP81.Features.Controls
{
    public class VideoPlayerViewModel : ReactiveRoutableBase
    {
        public virtual Uri VideoUri { get; set; }

        public VideoPlayerViewModel(IScreen hostScreen = null) : base(hostScreen) { }
    }
}
