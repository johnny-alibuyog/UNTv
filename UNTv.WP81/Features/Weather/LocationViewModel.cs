using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.Features.Weather
{
    public class LocationViewModel : ReactiveBase
    {
        public virtual string Place { get; set; }

        public virtual Nullable<DateTimeOffset> Timestamp { get; set; }

        public virtual string TimeZone { get; set; }

        public virtual double Longitude { get; set; }

        public virtual double Latitude { get; set; }
    }
}
