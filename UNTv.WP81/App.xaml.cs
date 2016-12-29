using ReactiveUI;
using Splat;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace UNTv.WP81
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private AutoSuspendHelper _autoSuspendHelper;
        private TransitionCollection _transitions;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;

            _autoSuspendHelper = new AutoSuspendHelper(this);
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            //DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            StatusBar.GetForCurrentView().HideAsync();
            Bootstrapper.Start(); // let all resource load first before creating objects

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content, // just ensure that the window is active
            if (rootFrame == null)
            {

                rootFrame = new Frame();    // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame.CacheSize = 1;    // TODO: change this value to a cache size that is appropriate for your application

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                Window.Current.Content = rootFrame; // Place the frame in the current Window
            }

            if (rootFrame.Content == null)
            {
                if (rootFrame.ContentTransitions != null) // Removes the turnstile navigation for startup.
                {
                    this._transitions = new TransitionCollection();
                    foreach (var item in rootFrame.ContentTransitions)
                    {
                        this._transitions.Add(item);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation parameter
                if (!rootFrame.Navigate(typeof(ShellView), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this._transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Hadware back button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var router = Locator.CurrentMutable.GetService<IScreen>().Router;
            if (router.NavigateBack.CanExecute(null))
            {
                router.NavigateBack.Execute(null);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved without knowing 
        /// whether the application will be terminated or resumed with the contents of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete(); // TODO: Save application state and stop any background activity
        }
    }
}