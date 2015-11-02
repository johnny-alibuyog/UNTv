using System;
using ReactiveUI;
using UNTv.WP81.Features.About;
using UNTv.WP81.Features.ContactUs;
using UNTv.WP81.Features.News;
using UNTv.WP81.Features.PublicServices;
using UNTv.WP81.Features.Start;
using UNTv.WP81.Features.Videos;

namespace UNTv.WP81.Features
{
    public class MainHubViewModel : ReactiveRoutableBase
    {
        public virtual StartSectionViewModel StartSection { get; set; }
        public virtual NewsSectionViewModel NewsSection { get; set; }
        public virtual VideosSectionViewModel VideosSection { get; set; }
        public virtual PublicServicesSectionViewModel PublicServicesSection { get; set; }
        public virtual Radios.ProgramsSectionViewModel RadioProgramSection { get; set; }
        public virtual Televisions.ProgramsSectionViewModel TelevisionProgramSection { get; set; }
        public virtual AboutSectionViewModel AboutSection { get; set; }
        public virtual ContactUsSectionViewModel ContactUsSection { get; set; }

        //public virtual ReactiveCommand<object> InitializeCommand { get; set; }

        public virtual ReactiveCommand<object> PopulateCommand { get; set; }

        public MainHubViewModel(IScreen hostScreen = null)
            : base(hostScreen)
        {
            this.StartSection = new StartSectionViewModel();
            this.NewsSection = new NewsSectionViewModel();
            this.VideosSection = new VideosSectionViewModel();
            this.PublicServicesSection = new PublicServicesSectionViewModel();
            this.RadioProgramSection = new Radios.ProgramsSectionViewModel();
            this.TelevisionProgramSection = new Televisions.ProgramsSectionViewModel();
            this.AboutSection = new AboutSectionViewModel();
            this.ContactUsSection = new ContactUsSectionViewModel();

            //this.InitializeCommand = ReactiveCommand.Create();
            //this.InitializeCommand.Subscribe(x => Initialize());

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate());
        }

        private void Populate()
        {
            this.StartSection.PopulateCommand.Execute(null);
            this.NewsSection.PopulateCommand.Execute(null);
            this.VideosSection.PopulateCommand.Execute(null);
            this.PublicServicesSection.PopulateCommand.Execute(null);
            this.RadioProgramSection.PopulateCommand.Execute(null);
            this.TelevisionProgramSection.PopulateCommand.Execute(null);
            this.AboutSection.PopulateCommand.Execute(null);
            this.ContactUsSection.PopulateCommand.Execute(null);
        }

        //public void Initialize()
        //{
        //    //this.NewsSection = new NewsSectionViewModel();
        //    //this.VideosSection = new VideosSectionViewModel();
        //    //this.PublicServicesSection = new PublicServicesSectionViewModel();
        //    //this.RadioProgramSection = new Radios.ProgramsSectionViewModel();
        //    //this.TelevisionProgramSection = new Televisions.ProgramsSectionViewModel();
        //}
    }
}
