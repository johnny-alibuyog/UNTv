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
