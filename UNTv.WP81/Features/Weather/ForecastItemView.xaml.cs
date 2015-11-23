using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ReactiveUI;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
