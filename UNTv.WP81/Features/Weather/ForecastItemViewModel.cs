namespace UNTv.WP81.Features.Weather
{
    public class ForecastItemDetailViewModel : ReactiveBase
    {
        public virtual string Name { get; set; }

        public virtual string Details { get; set; }

        public virtual string ImageUri { get; set; }
    }

    public class ForecastItemViewModel : ReactiveBase
    {
        public virtual string Title { get; set; }

        public virtual ForecastItemDetailViewModel Day { get; set; }

        public virtual ForecastItemDetailViewModel Night { get; set; }
    }
}
