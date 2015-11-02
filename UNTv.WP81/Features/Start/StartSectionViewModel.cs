using System;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Contracts.Services;
using UNTv.WP81.Features.Controls;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Features.Start
{
    public class StartSectionViewModel : ReactiveBase
    {
        private readonly IStore _webStore;
        private readonly IStore _localStore;
        private readonly RoutingState _router;

        public virtual ItemViewModel FeaturedProgram { get; set; }
        public virtual ReactiveList<ItemViewModel> Programs { get; set; }
        public virtual ReactiveCommand<object> PopulateCommand { get; set; }
        public virtual ReactiveCommand<object> NavigateToAudioStreamingCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToVideoStreamingCommand { get; private set; }
        public virtual ReactiveCommand<object> NavigateToFeaturedProgramCommand { get; private set; }

        public StartSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _webStore = Locator.CurrentMutable.GetService<WebStore>();
            _localStore = Locator.CurrentMutable.GetService<LocalStore>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToAudioStreamingCommand = ReactiveCommand.Create();
            this.NavigateToAudioStreamingCommand.Subscribe(x => NavigateToAudioSreaming());

            this.NavigateToVideoStreamingCommand = ReactiveCommand.Create();
            this.NavigateToVideoStreamingCommand.Subscribe(x => NavigateToVideoSreaming());

            this.NavigateToFeaturedProgramCommand = ReactiveCommand.Create(this.WhenAny(x => x.FeaturedProgram, x => x != null));
            this.NavigateToFeaturedProgramCommand.Subscribe(x => NavigateToFeaturedProgram());
        }

        private void Populate()
        {
            //Task.Factory.StartNew(() => Populate(_localStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            //if (!NetworkInterface.GetIsNetworkAvailable())
            //    return;

            //Task.Factory.StartNew(() => Populate(_webStore), CancellationToken.None,
            //    TaskCreationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());

            //Populate(_localStore);
            Populate(_webStore);
        }

        private void Populate(IStore store)
        {
            if (this.Programs.IsNullOrEmpty())
            {
                store.Get(new TelevisionProgramMessage.Request()).ContinueWith(
                    continuationAction: x => PopulatePrograms(x.Result.AsItems()),
                    scheduler: TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
        }

        private void PopulatePrograms(ReactiveList<ItemViewModel> programs)
        {
            this.Programs = programs;
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
            //_router.Navigate.Execute(new NewsHubViewModel());
        }

        private void NavigateToVideoSreaming()
        {
            _router.Navigate.Execute(new HlsPlayerViewModel(videoUri: new Uri("http://livestream01.untvweb.com:1935/public/untvwebstream/playlist.m3u8")));
        }

        private void NavigateToFeaturedProgram()
        {
            _router.Navigate.Execute(this.FeaturedProgram);
        }

    }
}
