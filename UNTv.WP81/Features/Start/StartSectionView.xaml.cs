using ReactiveUI;
using System;
using System.Reactive.Linq;
using UNTv.WP81.Common.Extentions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Start
{
    public sealed partial class StartSectionView : UserControl, IViewFor<StartSectionViewModel>
    {
        private bool _isActivated;
        private DispatcherTimer _timer;

        public StartSectionView()
        {
            this.InitializeComponent();
            this.InitializeBinding();
        }

        private void InitializeBinding()
        {

            this.WhenActivated(block =>
            {
                if (_isActivated)
                    return;

                this.BindCommand(ViewModel, x => x.NavigateToAudioStreamingCommand, x => x.AudioStreamingButton);
                this.BindCommand(ViewModel, x => x.NavigateToVideoStreamingCommand, x => x.VideoStreamingButton);
                this.Bind(ViewModel, x => x.FeaturedProgram, x => x.FeaturedProgramPhoto.DataContext);

                this.FeaturedProgramPhoto.Events().Tapped
                    .Select(x => x.OriginalSource)
                    .Where(x => this.ViewModel.NavigateToFeaturedProgramCommand.CanExecute(null))
                    .Subscribe(x => this.ViewModel.NavigateToFeaturedProgramCommand.Execute(null));

                this.ViewModel.WhenAnyValue(x => x.Programs)
                    .Where(x => !x.IsNullOrEmpty() && _timer == null)
                    .Subscribe(x => SetupImageRandomizer());

                this.ViewModel.PopulateCommand.Execute(null);

                _isActivated = true;
            });
        }

        private void SetupImageRandomizer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += (sender, args) =>
            {
                if (this.ViewModel.ChangeFeaturedProgramCommand.CanExecute(null))
                    this.ViewModel.ChangeFeaturedProgramCommand.Execute(null);
            };
            _timer.Interval = new TimeSpan(0, 0, 6);
            _timer.Start();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                .Register("ViewModel", typeof(StartSectionViewModel), typeof(StartSectionView), new PropertyMetadata(null));

        public StartSectionViewModel ViewModel
        {
            get { return (StartSectionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (StartSectionViewModel)value; }
        }
    }
}
