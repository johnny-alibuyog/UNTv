using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class DetailView : Page, IViewFor<DetailViewModel>
    {
        public DetailView()
        {
            this.InitializeComponent();
            this.InitializeBinding();
        }

        private void InitializeBinding()
        {
            this.WhenActivated(block =>
            {
                //block(this.OneWayBind(ViewModel, x => x.Title, x => x.TitleTextBlock.Text));
                //block(this.Bind(ViewModel, x => x.Content, x => x.ContentGridView));

                DataContext = this.ViewModel;
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(DetailViewModel), typeof(DetailView), new PropertyMetadata(null));

        public DetailViewModel ViewModel
        {
            get { return (DetailViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (DetailViewModel)value; }
        }
    }
}
