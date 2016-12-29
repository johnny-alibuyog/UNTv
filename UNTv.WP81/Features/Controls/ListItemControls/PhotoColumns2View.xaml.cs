using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class PhotoColumns2View : UserControl, IViewFor<PhotoColumns2ViewModel>
    {
        public PhotoColumns2View()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                 .Register("ViewModel", typeof(PhotoColumns2ViewModel), typeof(PhotoColumns2View), new PropertyMetadata(null));

        public PhotoColumns2ViewModel ViewModel
        {
            get { return (PhotoColumns2ViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PhotoColumns2ViewModel)value; }
        }
    }
}
