using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainHubView : Page, IViewFor<MainHubViewModel>
    {
        //private static int _currentHubIndex;

        public MainHubView()
        {
            this.InitializeComponent();
            this.InitializeBindings();
        }

        private void InitializeBindings()
        {
            this.WhenActivated(block =>
            {

            });

            //this.Bind(ViewModel, x => x.Items, x => x.NewsSection.DataContext);

            //this.WhenAnyValue(x => x.ViewModel.GetDataCommand)
            //    .SelectMany(x => x.ExecuteAsync())
            //    .Subscribe();
            //this.DataContext = this.ViewModel;

            //this.BindCommand(ViewModel, x => x.GoToBasicCommand, x => x.GoToBasicButton);
            //this.Bind(ViewModel, x => x.Name, x => x.NameTextBlock.Text);
            //this.Bind(ViewModel, x => x.Address, x => x.AddressTextBlock.Text);

            //this.Bind(ViewModel, x => x.Name, x => x.NameTextBox.Text);
            //this.Bind(ViewModel, x => x.Address, x => x.AddressTextBox.Text);

            //this.OneWayBind(ViewModel, x => x.Items, x => x.NewsListView.ItemsSource);
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register("ViewModel", typeof(MainHubViewModel), typeof(MainHubView), new PropertyMetadata(null));

        public MainHubViewModel ViewModel
        {
            get { return (MainHubViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainHubViewModel)value; }
        }
    }
}
