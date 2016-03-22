using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using UNTv.WP81.Common.Extentions;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UNTv.WP81.Features
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainHubView : Page, IViewFor<MainHubViewModel>
    {
        //private static int _currentHubIndex;
        private static bool _hasNotifiedWithOfflineMode;

        public MainHubView()
        {
            this.InitializeComponent();
            this.InitializeBindings();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.Loaded += (sender, args) => CheckInternetAccess();
        }

        private void InitializeBindings()
        {
            this.WhenActivated(block =>
            {
                Func<IEnumerable<ReactiveBase>> GetVisibleSections = () => this.SectionHub.SectionsInView.Select(x => x.DataContext as ReactiveBase);

                // capture SectionInViewChanged event
                this.SectionHub.Events().SectionsInViewChanged
                    .Where(x => GetVisibleSections().Count() == 2)
                    .Subscribe(x => this.ViewModel.CurrentSection = GetVisibleSections().First());

                this.DataContext = this.ViewModel;

                this.ViewModel.PopulateCommand.Execute(null);

                //this.ViewModel.WhenAnyValue(x => x.IsLoading)
                //    .Subscribe(isLoading => 
                //    {
                //        var statusBar =StatusBar.GetForCurrentView();
                //        if (isLoading)
                //            statusBar.ShowAsync();
                //        else
                //            statusBar.HideAsync();
                //    });

            });
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

        private async Task CheckInternetAccess()
        {
            if (_hasNotifiedWithOfflineMode)
                return;

            if (NetworkExtention.HasInternetConnection())
                return;

            await new MessageDialog("No internet connection is avaliable. UNTv App will run on off-line mode.").ShowAsync();
            _hasNotifiedWithOfflineMode = true;
        }
    }
}
