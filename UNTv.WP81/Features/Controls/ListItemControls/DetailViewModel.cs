using ReactiveUI;
using Splat;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public class DetailViewModel : ItemViewModel, IRoutableViewModel
    {
        public virtual IScreen HostScreen { get; protected set; }

        public virtual string UrlPathSegment { get; protected set; }

        public DetailViewModel(IScreen hostScreen = null, ItemViewModel value = null)
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
                this.Content = value.Content;
            }
        }
    }
}
