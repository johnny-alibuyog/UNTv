using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class PhotoColumns3View : UserControl, IViewFor<PhotoColumns3ViewModel>
    {
        public PhotoColumns3View()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                .Register("ViewModel", typeof(PhotoColumns3ViewModel), typeof(PhotoColumns3View), new PropertyMetadata(null));

        public PhotoColumns3ViewModel ViewModel
        {
            get { return (PhotoColumns3ViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PhotoColumns3ViewModel)value; }
        }
    }
}
