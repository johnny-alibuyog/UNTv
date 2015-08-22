using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using MyToolkit.Multimedia;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public class VideoPlayerViewModel: ItemViewModel, IRoutableViewModel
    {
        public virtual Uri VideoUri { get; set; }
        public virtual IScreen HostScreen { get; protected set; }
        public virtual string UrlPathSegment { get; protected set; }
        public virtual ReactiveCommand<object> ResolveVideoUriCommand { get; set; }

        public VideoPlayerViewModel(IScreen hostScreen = null, ItemViewModel value = null)
        {
            this.HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            if (value != null)
            {
                this.Id = value.Id;
                this.Title = value.Title;
                this.Subtitle = value.Subtitle;
                this.Category = value.Category;
                this.Description = value.Description;
                this.ImageUri = value.ImageUri;
                //this.VideoUri = YouTube.GetVideoUriAsync(value.Content, YouTubeQuality.Quality480P).Wait(); //string.Format(@"http://www.youtube.com/embed/{0}?rel=0&fs=0", value.Content);
                this.Content = value.Content;

                YouTube.GetVideoUriAsync(value.Content, YouTubeQuality.Quality480P).ContinueWith(
                    continuationAction: x => this.VideoUri = x.Result.Uri,
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }
    }
}
