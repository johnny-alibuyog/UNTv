using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class PhotoSizeMediumView : UserControl, IViewFor<PhotoSizeMediumViewModel>
    {
        public PhotoSizeMediumView()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                 .Register("ViewModel", typeof(PhotoSizeMediumViewModel), typeof(PhotoSizeMediumView), new PropertyMetadata(null));

        public PhotoSizeMediumViewModel ViewModel
        {
            get { return (PhotoSizeMediumViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PhotoSizeMediumViewModel)value; }
        }
    }
}
