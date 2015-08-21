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

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class PhotoColumns3View : UserControl, IViewFor<PhotoColumns3ViewModel>
    {
        public PhotoColumns3View()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                .Register("ViewModel", typeof(PhotoColumns3ViewModel), typeof(PhotoColumns3View), new PropertyMetadata(null));

        public PhotoColumns3ViewModel ViewModel
        {
            get { return (PhotoColumns3ViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PhotoColumns3ViewModel)value; }
        }
    }
}
