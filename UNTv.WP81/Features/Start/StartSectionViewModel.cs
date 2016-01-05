using System;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Features.Controls.ListItemControls;
using UNTv.WP81.Features.Controls.MediaPlayerControls.Hls;

namespace UNTv.WP81.Features.Start
{
    public class StartSectionViewModel : ReactiveBase
    {
        private Nullable<bool> _isDataLocal;
        private readonly RoutingState _router;
        private readonly IDataService _service;

        public virtual Uri AudioUri { get; set; }
        public virtual Uri VideoUri { get; set; }
        public virtual ItemViewModel FeaturedProgram { get; set; }
        public virtual ReactiveList<ItemViewModel> Programs { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> ChangeFeaturedProgramCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToAudioStreamingCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToVideoStreamingCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToFeaturedProgramCommand { get; private set; }

        public StartSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _service = Locator.CurrentMutable.GetService<IDataService>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.ChangeFeaturedProgramCommand = ReactiveCommand.Create(this.WhenAny(x => x.Programs, x => !x.Value.IsNullOrEmpty()));
            this.ChangeFeaturedProgramCommand.Subscribe(x => ChangeFeaturedPrograms());

            this.NavigateToAudioStreamingCommand = ReactiveCommand.Create(this.WhenAny(x => x.AudioUri, x => x.Value != null));
            this.NavigateToAudioStreamingCommand.Subscribe(x => NavigateToAudioSreaming());

            this.NavigateToVideoStreamingCommand = ReactiveCommand.Create(this.WhenAny(x => x.VideoUri, x => x.Value != null));
            this.NavigateToVideoStreamingCommand.Subscribe(x => NavigateToVideoSreaming());

            this.NavigateToFeaturedProgramCommand = ReactiveCommand.Create(this.WhenAny(x => x.FeaturedProgram, x => x.Value != null));
            this.NavigateToFeaturedProgramCommand.Subscribe(x => NavigateToFeaturedProgram());
        }

        private void Populate()
        {
            if (this.Programs.IsNullOrEmpty())
            {
                _service.Get(new TelevisionProgramMessage.Request()).ContinueWith(
                    continuationAction: x => PopulateProgram(x.Result.AsItems()),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.AudioUri == null)
            {
                _service.Get(new StreamingMessage.Request(StreamingMedia.Audio)).ContinueWith(
                    continuationAction: x => this.AudioUri = x.Result.Uri,
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            if (this.VideoUri == null)
            {
                _service.Get(new StreamingMessage.Request(StreamingMedia.Video)).ContinueWith(
                    continuationAction: x => this.VideoUri = x.Result.Uri,
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }

        private void PopulateProgram(ReactiveList<ItemViewModel> programs)
        {
            this.Programs = programs;
            this.FeaturedProgram = GetRandomProgam();
        }

        private void ChangeFeaturedPrograms()
        {
            this.FeaturedProgram = GetRandomProgam();
        }

        private ItemViewModel GetRandomProgam()
        {
            if (this.Programs == null)
                return null;

            var random = new Random();
            var index = random.Next(0, this.Programs.Count - 1);

            return this.Programs[index];
        }

        private void NavigateToAudioSreaming()
        {
            _router.Navigate.Execute(new HlsPlayerViewModel(mediaUri: this.AudioUri));
        }

        private void NavigateToVideoSreaming()
        {
            _router.Navigate.Execute(new HlsPlayerViewModel(mediaUri: this.VideoUri));
        }

        private void NavigateToFeaturedProgram()
        {
            _router.Navigate.Execute(new DetailViewModel(value: this.FeaturedProgram));
        }

    }
}
