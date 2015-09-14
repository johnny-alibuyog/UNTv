using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using UNTv.WP81.Data.Entities;
using UNTv.WP81.Features.Controls.ListItemControls;

namespace UNTv.WP81.Data.Contracts.Messages
{
    public static class RadioProgramScheduleMessage
    {
        public class Request : IReturn<Response> { }

        public class Response
        {
            [JsonProperty("status")]
            public virtual string Status { get; set; }

            public virtual RadioProgramSchedule[] MondayPrograms { get; set; }

            public virtual RadioProgramSchedule[] TuesdayPrograms { get; set; }

            public virtual RadioProgramSchedule[] WednesdayPrograms { get; set; }

            public virtual RadioProgramSchedule[] ThursdayPrograms { get; set; }

            public virtual RadioProgramSchedule[] FridayPrograms { get; set; }

            public virtual RadioProgramSchedule[] SaturdayPrograms { get; set; }

            public virtual RadioProgramSchedule[] SundayPrograms { get; set; }

            [JsonProperty("programs")]
            private JArray RawData { get; set; }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext context)
            {
                var weeks = RawData[0];

                this.MondayPrograms = JsonConvert.DeserializeObject<RadioProgramSchedule[]>(weeks["monday"].ToString());
                this.TuesdayPrograms = JsonConvert.DeserializeObject<RadioProgramSchedule[]>(weeks["tuesday"].ToString());
                this.WednesdayPrograms = JsonConvert.DeserializeObject<RadioProgramSchedule[]>(weeks["wednesday"].ToString());
                this.ThursdayPrograms = JsonConvert.DeserializeObject<RadioProgramSchedule[]>(weeks["thursday"].ToString());
                this.FridayPrograms = JsonConvert.DeserializeObject<RadioProgramSchedule[]>(weeks["friday"].ToString());
                this.SaturdayPrograms = JsonConvert.DeserializeObject<RadioProgramSchedule[]>(weeks["saturday"].ToString());
                this.SundayPrograms = JsonConvert.DeserializeObject<RadioProgramSchedule[]>(weeks["sunday"].ToString());
            }

            public Response()
            {
                this.MondayPrograms = new RadioProgramSchedule[] { };
                this.TuesdayPrograms = new RadioProgramSchedule[] { };
                this.WednesdayPrograms = new RadioProgramSchedule[] { };
                this.ThursdayPrograms = new RadioProgramSchedule[] { };
                this.FridayPrograms = new RadioProgramSchedule[] { };
                this.SaturdayPrograms = new RadioProgramSchedule[] { };
                this.SundayPrograms = new RadioProgramSchedule[] { };
            }

            public virtual ReactiveList<ItemViewModel> AsItems(DayOfWeek day)
            {
                Func<RadioProgramSchedule[]> GetPrograms = () =>
                {
                    switch (day)
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

                Func<RadioProgramSchedule, string> ChoseFromImages = (x) =>
                    ValidateUri(x.ThumbnailUri) ? x.ThumbnailUri :
                    ValidateUri(x.BannerThumbnailUri) ? x.BannerThumbnailUri :
                    ValidateUri(x.BannerUri) ? x.BannerUri : "/Assets/Images/LightGray.png";

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

}
