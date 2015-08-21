using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Reactive;
using System.Reactive.Linq;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace UNTv.WP81.Features
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HubPageView : Page, IViewFor<HubPageViewModel>
    {
        public HubPageView()
        {
            this.InitializeComponent();
            this.InitializeBindings();
        }

        private void InitializeBindings()
        {
            this.Bind(ViewModel, x => x.Items, x => x.NewsSection.DataContext);

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
            .Register("ViewModel", typeof(HubPageViewModel), typeof(HubPageView), new PropertyMetadata(null));

        public HubPageViewModel ViewModel
        {
            get { return (HubPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (HubPageViewModel)value; }
        }
    }
}
