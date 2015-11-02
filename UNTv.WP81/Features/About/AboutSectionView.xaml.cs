using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.About
{
    public sealed partial class AboutSectionView : UserControl, IViewFor<AboutSectionViewModel>
    {
        public AboutSectionView()
        {
            this.InitializeComponent();
            this.InitializeBinding();
        }

        private void InitializeBinding()
        {
            this.WhenActivated(block =>
            {
                DataContext = this.ViewModel;
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(AboutSectionViewModel), typeof(AboutSectionView), new PropertyMetadata(null));

        public AboutSectionViewModel ViewModel
        {
            get { return (AboutSectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (AboutSectionViewModel)value; }
        }
    }
}
