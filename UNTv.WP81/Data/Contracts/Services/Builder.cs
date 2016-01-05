using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class Builder : IBuilder
    {
        private int _defaultCount = 20;

        private class TelevisionUri
        {
            private static string _baseUri = "http://www.untvweb.com";

            public static string News = _baseUri + "/news/api/get_category_posts/?callback=?&count={0}&slug={1}";
            public static string Videos = _baseUri + "/api/programs/get_sorted_video_list/?sortby={0}&numposts={1}";
            public static string PublicServices = _baseUri + "/api/programs/get_page_children_shallow/?slug=advocacies&callback=?";
            public static string Programs = _baseUri + "/api/programs/get_programs/?callback=?";
            public static string ProgramShcedules = _baseUri + "/api/programs/get_all_programs/?callback=?";
            public static string Streaming = _baseUri + "/api/get_recent_posts/?callback=?&post_type=Streaming";
        }

        private class RadioUri
        {
            private static string _baseUri = "http://www.untvradio.com/";

            public static string Programs = _baseUri + "/api/programs/get_programs/?callback=?";
            public static string ProgramShcedules = _baseUri + "/api/programs/get_all_programs/?callback=?";
            public static string Streaming = _baseUri + "/api/get_recent_posts/?callback=?&post_type=Streaming";
        }

        private class WeatherUri
        {
            public static string WUKey = "64b984ed41d048ec";
            public static string Forecast = "http://api.wunderground.com/api/{0}/conditions/forecast/geolookup/q/{1},{2}.json";
        }

        public string BuildUri<T>(IReturn<T> request) where T : class
        {
            if (request is NewsMessage.Request)
                return string.Format(TelevisionUri.News, ((NewsMessage.Request)request).Count ?? _defaultCount, ((NewsMessage.Request)request).Category.Slug);

            if (request is VideoMessage.Request)
                return string.Format(TelevisionUri.Videos, ((VideoMessage.Request)request).SortBy.ToString().ToLower(), ((VideoMessage.Request)request).Count ?? _defaultCount);

            if (request is PublicServiceMessage.Request)
                return TelevisionUri.PublicServices;

            if (request is RadioProgramMessage.Request)
                return RadioUri.Programs;

            if (request is RadioProgramScheduleMessage.Request)
                return RadioUri.ProgramShcedules;

            if (request is TelevisionProgramMessage.Request)
                return TelevisionUri.Programs;

            if (request is TelevisionProgramScheduleMessage.Request)
                return TelevisionUri.ProgramShcedules;

            if (request is VideoMessage.Request)
                return TelevisionUri.Videos;

            if (request is WeatherMessage.Request)
                return string.Format(WeatherUri.Forecast, WeatherUri.WUKey, ((WeatherMessage.Request)request).Latitude, ((WeatherMessage.Request)request).Longitude);

            if (request is StreamingMessage.Request)
            {
                var streamingRequest = (StreamingMessage.Request)request;
                switch (((StreamingMessage.Request)request).Media)
                {
                    case StreamingMedia.Audio:
                        return RadioUri.Streaming;
                    case StreamingMedia.Video:
                        return TelevisionUri.Streaming;
                }
            }

            return null;
        }

        public string BuildFilename<T>(IReturn<T> request) where T : class
        {
            if (request is NewsMessage.Request)
                return string.Format("news_{0}_{1}.json", ((NewsMessage.Request)request).Category.Slug, ((NewsMessage.Request)request).Count ?? _defaultCount);

            if (request is VideoMessage.Request)
                return string.Format("videos_{0}_{1}.json", ((VideoMessage.Request)request).SortBy.ToString().ToLower(), ((VideoMessage.Request)request).Count ?? _defaultCount);

            if (request is PublicServiceMessage.Request)
                return "public_services.json";

            if (request is RadioProgramMessage.Request)
                return "radio_programs.json";

            if (request is RadioProgramScheduleMessage.Request)
                return "radio_program_schedules.json";

            if (request is TelevisionProgramMessage.Request)
                return "television_programs.json";

            if (request is TelevisionProgramScheduleMessage.Request)
                return "television_program_schedules.json";

            if (request is WeatherMessage.Request)
                return "weather_forcast.json";

            if (request is StreamingMessage.Request)
            {
                var streamingRequest = (StreamingMessage.Request)request;
                switch (((StreamingMessage.Request)request).Media)
                {
                    case StreamingMedia.Audio:
                        return "audio_streaming.json";
                    case StreamingMedia.Video:
                        return "video_streaming.json";
                }
            }

            return null;
        }
    }
}
