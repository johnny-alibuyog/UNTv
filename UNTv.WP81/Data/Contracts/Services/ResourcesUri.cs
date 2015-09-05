using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.Data.Contracts.Services
{
    public static class ResourcesUri
    {
        public static class Television
        {
            private static string _baseUri = "http://www.untvweb.com";

            public static string News = _baseUri + "/news/api/get_category_posts/?callback=?&count={0}&slug={1}";
            public static string Videos = _baseUri + "/api/programs/get_sorted_video_list/?sortby={0}&numposts={1}";
            public static string PubliServices = _baseUri + "/api/programs/get_page_children_shallow/?slug=advocacies&callback=?";
            public static string Programs = _baseUri + "/api/programs/get_programs/?callback=?";
            public static string ProgramShcedules = _baseUri + "/api/programs/get_all_programs/?callback=?";
        }

        public static class Radio
        {
            private static string _baseUri = "http://www.untvradio.com/";

            public static string Programs = _baseUri + "/api/programs/get_programs/?callback=?";
            public static string ProgramShcedules = _baseUri + "/api/programs/get_all_programs/?callback=?";
        }
    }
}
