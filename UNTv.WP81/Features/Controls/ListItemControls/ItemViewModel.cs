using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNTv.WP81.Features;
using Windows.UI.Xaml.Media.Imaging;

namespace UNTv.WP81.Features.Controls.ListItemControls
{


    public class ItemViewModel : ReactiveBase
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string Content { get; set; }

        public BitmapImage BitmapImage
        {
            get { return new BitmapImage(new Uri(ImagePath)); }
        }
    }
}
