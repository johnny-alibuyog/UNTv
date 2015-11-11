using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using UNTv.WP81.Common.Extentions;
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
        public virtual bool IsLoading { get; set; }
        public virtual ReactiveBase CurrentSection { get; set; }
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
            this.PopulateCommand.Subscribe(x => Populate(x));

            this.CurrentSection = this.StartSection;
            this.WhenAnyValue(x => x.CurrentSection)
                .Subscribe(x => this.PopulateCommand.Execute(x));

            this.CurrentSection = this.StartSection;

            // Setup progress bar
            this.StartSection.WhenAnyValue(x => x.Programs)
                .Where(x => this.CurrentSection == this.StartSection)
                .Select(programs => programs == null)
                .Subscribe(x => this.IsLoading = x);
                //.ToProperty(this, x => x.IsLoading);

            this.NewsSection.WhenAnyValue(x => x.News)
                .Where(x => this.CurrentSection == this.NewsSection)
                .Select(news => news == null)
                .Subscribe(x => this.IsLoading = x);
                //.ToProperty(this, x => x.IsLoading);

            this.VideosSection.WhenAnyValue(x => x.Videos)
                .Where(x => this.CurrentSection == this.VideosSection)
                .Select(videos => videos == null)
                .Subscribe(x => this.IsLoading = x);
                //.ToProperty(this, x => x.IsLoading);

            this.PublicServicesSection.WhenAnyValue(x => x.Programs)
                .Where(x => this.CurrentSection == this.PublicServicesSection)
                .Select(programs => programs == null)
                .Subscribe(x => this.IsLoading = x);
                //.ToProperty(this, x => x.IsLoading);

            this.RadioProgramSection.WhenAnyValue(x => x.Programs)
                .Where(x => this.CurrentSection == this.RadioProgramSection)
                .Select(programs => programs == null)
                .Subscribe(x => this.IsLoading = x);
                //.ToProperty(this, x => x.IsLoading);

            this.TelevisionProgramSection.WhenAnyValue(x => x.Programs)
                .Where(x => this.CurrentSection == this.TelevisionProgramSection)
                .Select(programs => programs == null)
                .Subscribe(x => this.IsLoading = x);
                //.ToProperty(this, x => x.IsLoading);

            this.AboutSection.WhenAnyValue(x => x.Content)
                .Where(x => this.CurrentSection == this.AboutSection)
                .Select(content => content == null)
                .Subscribe(x => this.IsLoading = x);
                //.ToProperty(this, x => x.IsLoading);

            this.ContactUsSection.WhenAnyValue(x => x.Content)
                .Where(x => this.CurrentSection == this.ContactUsSection)
                .Select(content => content == null)
                .Subscribe(x => this.IsLoading = x);
                //.ToProperty(this, x => x.IsLoading);
        }

        private void Populate(object section)
        {
            if (section == null || section == this.StartSection)
            {
                this.IsLoading = this.StartSection.Programs.IsNullOrEmpty();
                this.StartSection.PopulateCommand.Execute(null);
            }

            if (section == null || section == this.NewsSection)
            {
                this.IsLoading = this.NewsSection.News.IsNullOrEmpty();
                this.NewsSection.PopulateCommand.Execute(null);
            }

            if (section == null || section == this.VideosSection)
            {
                this.IsLoading = this.VideosSection.Videos.IsNullOrEmpty();
                this.VideosSection.PopulateCommand.Execute(null);
            }

            if (section == null || section == this.PublicServicesSection)
            {
                this.IsLoading = this.PublicServicesSection.Programs.IsNullOrEmpty();
                this.PublicServicesSection.PopulateCommand.Execute(null);
            }

            if (section == null || section == this.RadioProgramSection)
            {
                this.IsLoading = this.RadioProgramSection.Programs.IsNullOrEmpty();
                this.RadioProgramSection.PopulateCommand.Execute(null);
            }

            if (section == null || section == this.TelevisionProgramSection)
            {
                this.IsLoading = this.TelevisionProgramSection.Programs.IsNullOrEmpty();
                this.TelevisionProgramSection.PopulateCommand.Execute(null);
            }

            if (section == null || section == this.AboutSection)
            {
                this.IsLoading = string.IsNullOrEmpty(this.AboutSection.Content);
                this.AboutSection.PopulateCommand.Execute(null);
            }

            if (section == null || section == this.ContactUsSection)
            {
                this.IsLoading = string.IsNullOrEmpty(this.ContactUsSection.Content);
                this.ContactUsSection.PopulateCommand.Execute(null);
            }
        }
    }
}
