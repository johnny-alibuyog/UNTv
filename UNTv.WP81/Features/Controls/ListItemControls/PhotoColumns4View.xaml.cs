using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class PhotoColumns4View : UserControl, IViewFor<PhotoColumns4ViewModel>
    {
        public PhotoColumns4View()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                .Register("ViewModel", typeof(PhotoColumns4ViewModel), typeof(PhotoColumns4View), new PropertyMetadata(null));

        public PhotoColumns4ViewModel ViewModel
        {
            get { return (PhotoColumns4ViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PhotoColumns4ViewModel)value; }
        }
    }
}
