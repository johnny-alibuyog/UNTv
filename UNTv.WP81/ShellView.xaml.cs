using ReactiveUI;
using Splat;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UNTv.WP81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellView : Page, IViewFor<ShellViewModel>
    {
        public ShellView()
        {
            this.InitializeComponent();

            this.DataContext = Locator.Current.GetService<IScreen>();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(ShellViewModel), typeof(ShellView), new PropertyMetadata(null));

        public ShellViewModel ViewModel
        {
            get { return (ShellViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ShellViewModel)ViewModel; }
        }
    }
}
