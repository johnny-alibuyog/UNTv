using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Features.About;
using UNTv.WP81.Features.ContactUs;
using UNTv.WP81.Features.News;
using UNTv.WP81.Features.PublicServices;
using UNTv.WP81.Features.Start;
using UNTv.WP81.Features.Videos;
using UNTv.WP81.Features.Weather;

namespace UNTv.WP81.Features
{
    public class MainHubViewModel : ReactiveRoutableBase
    {
        public virtual bool IsLoading { get; set; }
        public virtual ReactiveBase CurrentSection { get; set; }
        public virtual StartSectionViewModel StartSection { get; set; }
        public virtual NewsSectionViewModel NewsSection { get; set; }
        public virtual VideosSectionViewModel VideosSection { get; set; }
        public virtual WeatherSectionViewModel WeatherSection { get; set; }
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
            this.StartSection = Locator.CurrentMutable.GetService<StartSectionViewModel>();
            this.NewsSection = Locator.CurrentMutable.GetService<NewsSectionViewModel>();
            this.VideosSection = Locator.CurrentMutable.GetService<VideosSectionViewModel>();
            this.WeatherSection = Locator.CurrentMutable.GetService<WeatherSectionViewModel>();
            this.PublicServicesSection = Locator.CurrentMutable.GetService<PublicServicesSectionViewModel>();
            this.RadioProgramSection = Locator.CurrentMutable.GetService<Radios.ProgramsSectionViewModel>();
            this.TelevisionProgramSection = Locator.CurrentMutable.GetService<Televisions.ProgramsSectionViewModel>();
            this.AboutSection = Locator.CurrentMutable.GetService<AboutSectionViewModel>();
            this.ContactUsSection = Locator.CurrentMutable.GetService<ContactUsSectionViewModel>();

            this.PopulateCommand = ReactiveCommand.Create();
            this.PopulateCommand.Subscribe(x => Populate(x));

            this.WhenAnyValue(x => x.CurrentSection)
                .Subscribe(x => this.PopulateCommand.Execute(x));

            // Setup progress bar
            this.StartSection.WhenAnyValue(x => x.Programs, x => x.AudioUri, x => x.VideoUri)
                .Where(x => this.CurrentSection == this.StartSection)
                .Subscribe(x => this.IsLoading = (x.Item1.IsNullOrEmpty() || x.Item2 == null || x.Item3 == null));

            this.NewsSection.WhenAnyValue(x => x.News)
                .Where(x => this.CurrentSection == this.NewsSection)
                .Subscribe(x => this.IsLoading = x == null);

            this.VideosSection.WhenAnyValue(x => x.Videos)
                .Where(x => this.CurrentSection == this.VideosSection)
                .Subscribe(x => this.IsLoading = x == null);

            this.WeatherSection.WhenAnyValue(x => x.Forecast)
                .Where(x => this.CurrentSection == this.WeatherSection)
                .Subscribe(x => this.IsLoading = x == null);

            this.PublicServicesSection.WhenAnyValue(x => x.Programs)
                .Where(x => this.CurrentSection == this.PublicServicesSection)
                .Subscribe(x => this.IsLoading = x == null);

            this.RadioProgramSection.WhenAnyValue(x => x.Programs)
                .Where(x => this.CurrentSection == this.RadioProgramSection)
                .Subscribe(x => this.IsLoading = x == null);

            this.TelevisionProgramSection.WhenAnyValue(x => x.Programs)
                .Where(x => this.CurrentSection == this.TelevisionProgramSection)
                .Subscribe(x => this.IsLoading = x == null);

            this.AboutSection.WhenAnyValue(x => x.Content)
                .Where(x => this.CurrentSection == this.AboutSection)
                .Subscribe(x => this.IsLoading = x == null);

            this.ContactUsSection.WhenAnyValue(x => x.Content)
                .Where(x => this.CurrentSection == this.ContactUsSection)
                .Subscribe(x => this.IsLoading = x == null);

            // populate start screen
            this.CurrentSection = this.StartSection;
        }

        private void Populate(object section)
        {
            if (section == this.StartSection)
            {
                this.IsLoading = this.StartSection.Programs.IsNullOrEmpty();
                this.StartSection.PopulateCommand.Execute(null);
            }
            else if (section == this.NewsSection)
            {
                this.IsLoading = this.NewsSection.News.IsNullOrEmpty();
                this.NewsSection.PopulateCommand.Execute(null);
            }
            else if (section == this.VideosSection)
            {
                this.IsLoading = this.VideosSection.Videos.IsNullOrEmpty();
                this.VideosSection.PopulateCommand.Execute(null);
            }
            else if (section == this.WeatherSection)
            {
                this.IsLoading = this.WeatherSection.Forecast.IsNullOrEmpty();
                if (this.WeatherSection.PopulateCommand.CanExecute(null))
                    this.WeatherSection.PopulateCommand.Execute(null);
            }
            else if (section == this.PublicServicesSection)
            {
                this.IsLoading = this.PublicServicesSection.Programs.IsNullOrEmpty();
                this.PublicServicesSection.PopulateCommand.Execute(null);
            }
            else if (section == this.RadioProgramSection)
            {
                this.IsLoading = this.RadioProgramSection.Programs.IsNullOrEmpty();
                this.RadioProgramSection.PopulateCommand.Execute(null);
            }
            else if (section == this.TelevisionProgramSection)
            {
                this.IsLoading = this.TelevisionProgramSection.Programs.IsNullOrEmpty();
                this.TelevisionProgramSection.PopulateCommand.Execute(null);
            }
            else if (section == this.AboutSection)
            {
                this.IsLoading = string.IsNullOrEmpty(this.AboutSection.Content);
                this.AboutSection.PopulateCommand.Execute(null);
            }
            else if (section == this.ContactUsSection)
            {
                this.IsLoading = string.IsNullOrEmpty(this.ContactUsSection.Content);
                this.ContactUsSection.PopulateCommand.Execute(null);
            }
        }
    }
}
