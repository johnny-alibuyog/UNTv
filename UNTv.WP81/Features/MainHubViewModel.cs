using System;
using ReactiveUI;
using UNTv.WP81.Features.News;
using UNTv.WP81.Features.PublicServices;
using UNTv.WP81.Features.Videos;

namespace UNTv.WP81.Features
{
    public class MainHubViewModel : ReactiveRoutableBase
    {
        //public virtual NewsSectionViewModel NewsSection { get; set; }

        //public virtual VideosSectionViewModel VideosSection { get; set; }

        //public virtual PublicServicesSectionViewModel PublicServicesSection { get; set; }

        //public virtual Radios.ProgramsSectionViewModel RadioProgramSection { get; set; }

        //public virtual Televisions.ProgramsSectionViewModel TelevisionProgramSection { get; set; }

        //public virtual ReactiveCommand<object> InitializeCommand { get; set; }

        public MainHubViewModel(IScreen hostScreen = null)
            : base(hostScreen)
        {
            //this.NewsSection = new NewsSectionViewModel();
            //this.VideosSection = new VideosSectionViewModel();
            //this.PublicServicesSection = new PublicServicesSectionViewModel();
            //this.RadioProgramSection = new Radios.ProgramsSectionViewModel();
            //this.TelevisionProgramSection = new Televisions.ProgramsSectionViewModel();

            //this.InitializeCommand = ReactiveCommand.Create();
            //this.InitializeCommand.Subscribe(x => Initialize());
        }

        public void Initialize()
        {
            //this.NewsSection = new NewsSectionViewModel();
            //this.VideosSection = new VideosSectionViewModel();
            //this.PublicServicesSection = new PublicServicesSectionViewModel();
            //this.RadioProgramSection = new Radios.ProgramsSectionViewModel();
            //this.TelevisionProgramSection = new Televisions.ProgramsSectionViewModel();
        }
    }
}
