using System;
using Windows.UI.Xaml.Media.Imaging;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public class ItemViewModel : ReactiveBase
    {
        public virtual long Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Subtitle { get; set; }

        public virtual string Category { get; set; }

        public virtual string Description { get; set; }

        public virtual string ImageUri { get; set; }

        public virtual string Content { get; set; }

        public virtual BitmapImage BitmapImage
        {
            get { return new BitmapImage(new Uri(ImageUri)); }
        }
    }
}
