using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.ContactUs
{
    public sealed partial class ContactUsSectionView : UserControl, IViewFor<ContactUsSectionViewModel>
    {
        public ContactUsSectionView()
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
            .Register("ViewModel", typeof(ContactUsSectionViewModel), typeof(ContactUsSectionView), new PropertyMetadata(null));

        public ContactUsSectionViewModel ViewModel
        {
            get { return (ContactUsSectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ContactUsSectionViewModel)value; }
        }
    }
}
