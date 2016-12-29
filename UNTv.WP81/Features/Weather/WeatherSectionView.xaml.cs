using ReactiveUI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Weather
{
    public sealed partial class WeatherSectionView : UserControl, IViewFor<WeatherSectionViewModel>
    {
        private bool _isActivated;

        public WeatherSectionView()
        {
            this.InitializeComponent();
            this.InitializeBindings();

            //Window.Current.WhenAnyValue(x => x.Content)
            //    .OfType<Page>()
            //    .Where(x => x != null)
            //    .Subscribe(x => x.Loaded += (sender, args) => this.GetLocation(sender));

            this.Loaded += WeatherSectionView_Loaded;
        }

        private void WeatherSectionView_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetLocation();
        }

        private void InitializeBindings()
        {


            this.WhenActivated(block =>
            {
                if (_isActivated)
                    return;

                this.Bind(ViewModel, x => x.Location, x => x.LocationPanel.DataContext);
                this.Bind(ViewModel, x => x.Forecast, x => x.ForecastListView.ItemsSource);

                this.ViewModel.WhenAnyValue(x => x.Location)
                    .Where(x => this.ViewModel.PopulateCommand.CanExecute(null))
                    .Subscribe(x => this.ViewModel.PopulateCommand.Execute(null));

                _isActivated = true;
            });
        }

        private async Task GetLocation()
        {
            try
            {
                var geolocator = new Geolocator();
                geolocator.DesiredAccuracyInMeters = 50;
                geolocator.MovementThreshold = 5;
                geolocator.ReportInterval = 2000;

                var geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(2),
                    timeout: TimeSpan.FromMinutes(5)
                );

                //this.ViewModel.Location = new LocationViewModel()
                //{
                //    //Country = geoposition.CivicAddress.Country,
                //    //City = geoposition.CivicAddress.City,
                //    //Timestamp = geoposition.CivicAddress.Timestamp,
                //    Longitude = geoposition.Coordinate.Point.Position.Longitude,
                //    Latitude = geoposition.Coordinate.Point.Position.Latitude
                //};

                this.ViewModel.Location.Longitude = geoposition.Coordinate.Point.Position.Longitude;
                this.ViewModel.Location.Latitude = geoposition.Coordinate.Point.Position.Latitude;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
             .Register("ViewModel", typeof(WeatherSectionViewModel), typeof(WeatherSectionView), new PropertyMetadata(null));

        public WeatherSectionViewModel ViewModel
        {
            get { return (WeatherSectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (WeatherSectionViewModel)value; }
        }
    }
}
