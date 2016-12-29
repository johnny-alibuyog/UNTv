using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class PhotoLeftView : UserControl, IViewFor<PhotoLeftViewModel>
    {
        public PhotoLeftView()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                 .Register("ViewModel", typeof(PhotoLeftViewModel), typeof(PhotoLeftView), new PropertyMetadata(null));

        public PhotoLeftViewModel ViewModel
        {
            get { return (PhotoLeftViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PhotoLeftViewModel)value; }
        }
    }
}
