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

namespace UNTv.WP81.Features.Controls.ListItemControls
{
    public sealed partial class DoubleLineView : UserControl, IViewFor<DoubleLineViewModel>
    {
        public DoubleLineView()
        {
            this.InitializeComponent();
            this.InitializeBinding();
        }

        private void InitializeBinding()
        {
            this.WhenActivated(block =>
            {
                block(this.OneWayBind(ViewModel, x => x.BitmapImage, x => x.ImagePath.Source));
                block(this.OneWayBind(ViewModel, x => x.Title, x => x.TitleTextBox.Text));
                block(this.OneWayBind(ViewModel, x => x.Subtitle, x => x.SubtitleTextBox.Text));
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                 .Register("ViewModel", typeof(DoubleLineViewModel), typeof(DoubleLineView), new PropertyMetadata(null));

        public DoubleLineViewModel ViewModel
        {
            get { return (DoubleLineViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (DoubleLineViewModel)value; }
        }
    }
}
