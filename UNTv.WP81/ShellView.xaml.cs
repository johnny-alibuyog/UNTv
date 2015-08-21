using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ReactiveUI;
using Splat;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
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
            //this.InitializeBindings();

            this.DataContext = Locator.Current.GetService<IScreen>();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        //private void InitializeBindings()
        //{
        //    //RxApp.SuspensionHost
        //    //    .ObserveAppState<MainViewModel>()
        //    //    .BindTo(this, x => x.ViewModel); 

        //    //this.WhenAnyValue(x => x.ViewModel.HostScreen.Router) 
        //    //    .Select(x => x.NavigateCommandFor<BasicViewModel>())
        //    //    .BindTo(this, x => x.GoToBasicButton.Command); 
        //}

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
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
