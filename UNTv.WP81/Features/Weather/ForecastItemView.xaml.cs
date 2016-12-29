using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace UNTv.WP81.Features.Weather
{
    public sealed partial class ForecastItemView : UserControl, IViewFor<ForecastItemViewModel>
    {
        public ForecastItemView()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                 .Register("ViewModel", typeof(ForecastItemViewModel), typeof(ForecastItemView), new PropertyMetadata(null));

        public ForecastItemViewModel ViewModel
        {
            get { return (ForecastItemViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ForecastItemViewModel)value; }
        }

    }
}
