using System;
using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class PhotoSizeLargeView : UserControl, IViewFor<PhotoSizeLargeViewModel>
    {
        public PhotoSizeLargeView()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                 .Register("ViewModel", typeof(PhotoSizeLargeViewModel), typeof(PhotoSizeLargeView), new PropertyMetadata(null));

        public PhotoSizeLargeViewModel ViewModel
        {
            get { return (PhotoSizeLargeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PhotoSizeLargeViewModel)value; }
        }

    }
}
