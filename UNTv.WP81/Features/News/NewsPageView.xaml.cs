using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ReactiveUI;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace UNTv.WP81.Features.News
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsPageView : Page, IViewFor<NewsPageViewModel>
    {
        public NewsPageView()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(NewsPageViewModel), typeof(NewsPageView), new PropertyMetadata(null));

        public NewsPageViewModel ViewModel
        {
            get { return (NewsPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (NewsPageViewModel)value; }
        }
    }
}
