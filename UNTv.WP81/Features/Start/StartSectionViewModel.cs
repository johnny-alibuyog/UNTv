using System;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
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

        public virtual ReactiveCommand<object> PopulateCommand { get; set; }

        public virtual ReactiveCommand<object> NavigateToAudioSreamingCommand { get; private set; }

        public virtual ReactiveCommand<object> NavigateToVideoSreamingCommand { get; private set; }

        public virtual ReactiveCommand<object> NavigateToFeaturedProgramCommand { get; private set; }

        public StartSectionViewModel()
        {
            _router = Locator.CurrentMutable.GetService<RoutingState>();
            _webStore = Locator.CurrentMutable.GetService<WebStore>();
            _localStore = Locator.CurrentMutable.GetService<LocalStore>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());

            this.NavigateToAudioSreamingCommand = ReactiveCommand.Create();
            this.NavigateToAudioSreamingCommand.Subscribe(x => NavigateToAudioSreaming());

            this.NavigateToVideoSreamingCommand = ReactiveCommand.Create();
            this.NavigateToVideoSreamingCommand.Subscribe(x => NavigateToVideoSreaming());

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

            Populate(_webStore);
        }

        private void Populate(IStore store)
        {
            store.Get(new TelevisionProgramMessage.Request()).ContinueWith(
                continuationAction: x => this.FeaturedProgram = GetRandomProgam(x.Result.AsItems()),
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private ItemViewModel GetRandomProgam(ReactiveList<ItemViewModel> programs)
        {
            var random = new Random();
            var index = random.Next(0, programs.Count - 1);
            return programs[index];
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
