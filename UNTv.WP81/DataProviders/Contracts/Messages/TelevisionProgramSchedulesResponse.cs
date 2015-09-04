using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using UNTv.WP81.DataProviders.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.DataProviders.Contracts.Messages
{
    public class TelevisionProgramSchedulesResponse
    {
        [JsonProperty("status")]
        public virtual string Status { get; set; }

        public virtual TelevisionProgramSchedule[] MondayPrograms { get; set; }

        public virtual TelevisionProgramSchedule[] TuesdayPrograms { get; set; }

        public virtual TelevisionProgramSchedule[] WednesdayPrograms { get; set; }

        public virtual TelevisionProgramSchedule[] ThursdayPrograms { get; set; }

        public virtual TelevisionProgramSchedule[] FridayPrograms { get; set; }

        public virtual TelevisionProgramSchedule[] SaturdayPrograms { get; set; }

        public virtual TelevisionProgramSchedule[] SundayPrograms { get; set; }

        [JsonProperty("programs")]
        private JArray RawData { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            var weeks = RawData[0];

            this.MondayPrograms = JsonConvert.DeserializeObject<TelevisionProgramSchedule[]>(weeks["monday"].ToString());
            this.TuesdayPrograms = JsonConvert.DeserializeObject<TelevisionProgramSchedule[]>(weeks["tuesday"].ToString());
            this.WednesdayPrograms = JsonConvert.DeserializeObject<TelevisionProgramSchedule[]>(weeks["wednesday"].ToString());
            this.ThursdayPrograms = JsonConvert.DeserializeObject<TelevisionProgramSchedule[]>(weeks["thursday"].ToString());
            this.FridayPrograms = JsonConvert.DeserializeObject<TelevisionProgramSchedule[]>(weeks["friday"].ToString());
            this.SaturdayPrograms = JsonConvert.DeserializeObject<TelevisionProgramSchedule[]>(weeks["saturday"].ToString());
            this.SundayPrograms = JsonConvert.DeserializeObject<TelevisionProgramSchedule[]>(weeks["sunday"].ToString());
        }

        public virtual ReactiveList<ItemViewModel> AsItems(DayOfWeek day)
        {
            Func<TelevisionProgramSchedule[]> GetPrograms = () =>
            {
                switch(day)
                {
                    case DayOfWeek.Monday:
                        return this.MondayPrograms;
                    case DayOfWeek.Tuesday:
                        return this.TuesdayPrograms;
                    case DayOfWeek.Wednesday:
                        return this.WednesdayPrograms;
                    case DayOfWeek.Thursday:
                        return this.ThursdayPrograms;
                    case DayOfWeek.Friday:
                        return this.FridayPrograms;
                    case DayOfWeek.Saturday:
                        return this.SaturdayPrograms;
                    case DayOfWeek.Sunday:
                        return this.SundayPrograms;
                    default:
                        return this.MondayPrograms;
                }
            };

            Predicate<string> ValidateUri = (stringUri) =>
            {
                var outUri = (Uri)null;
                return Uri.TryCreate(stringUri, UriKind.Absolute, out outUri);
            };

            Func<string, string> CreateImageTag = (uri) => string.Format("<img src=\"{0}\" \\> <br \\>", uri);

            Func<TelevisionProgramSchedule, string> ChoseFromImages = (x) =>
                ValidateUri(x.BannerUri) ? x.BannerUri :
                ValidateUri(x.ThumbnailUri) ? x.ThumbnailUri :
                ValidateUri(x.BannerThumbnailUri) ? x.BannerThumbnailUri : "/Assets/LightGray.png";

            var items = GetPrograms()
             .Select(x => new ItemViewModel()
             {
                 Id = x.Id,
                 Title = x.Title,
                 Subtitle = string.Format("{0} to {1}", x.StartTime, x.EndTime),
                 Category = "Television Programs Schedules",
                 ImageUri = ChoseFromImages(x),
                 Content = CreateImageTag(ChoseFromImages(x)) + x.Description
             });

            return new ReactiveList<ItemViewModel>(items);
        }
    }
}
