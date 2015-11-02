﻿using System;
using System.Reactive.Linq;
using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UNTv.WP81.Features.Start
{
    public sealed partial class StartSectionView : UserControl, IViewFor<StartSectionViewModel>
    {
        private bool _isActivated;

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
                block(this.OneWayBind(ViewModel, x => x.FeaturedProgram, x => x.FeaturedProgramContainer.DataContext));

                this.FeaturedProgramContainer.Events().Tapped
                    .Select(x => x.OriginalSource)
                    .Where(x => this.ViewModel.NavigateToFeaturedProgramCommand.CanExecute(null))
                    .Subscribe(x => this.ViewModel.NavigateToFeaturedProgramCommand.Execute(null));

                this.ViewModel.PopulateCommand.Execute(null);

                _isActivated = true;
            });
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
