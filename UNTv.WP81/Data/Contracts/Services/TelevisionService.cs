using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class TelevisionService : ServiceBase, ITelevisionService //: BaseRestService, IWebTvService
    {
        private readonly int _count = 20;

        public TelevisionService()
        {
            this.BaseAddress = new Uri("http://www.untvweb.com");
        }

        public async Task<NewsMessage.Response> Get(NewsMessage.Request request)
        {
            var query = string.Format("/news/api/get_category_posts/?callback=?&count={0}&slug={1}", request.Count ?? _count, request.Category);
            return await Get<NewsMessage.Response>(query);
        }

        public async Task<VideoMessage.Response> Get(VideoMessage.Request request)
        {
            var query = string.Format("/api/programs/get_sorted_video_list/?sortby={0}&numposts={1}", request.SortBy.ToString().ToLower(), request.Count ?? _count);
            return await Get<VideoMessage.Response>(query);
        }

        public async Task<PublicServiceMessage.Response> Get(PublicServiceMessage.Request request)
        {
            var query = string.Format("/api/programs/get_page_children_shallow/?slug=advocacies&callback=?");
            return await Get<PublicServiceMessage.Response>(query);
        }

        public async Task<TelevisionProgramMessage.Response> Get(TelevisionProgramMessage.Request request)
        {
            var query = string.Format("/api/programs/get_programs/?callback=?");
            return await Get<TelevisionProgramMessage.Response>(query);
        }

        public async Task<TelevisionProgramScheduleMessage.Response> Get(TelevisionProgramScheduleMessage.Request request)
        {
            var query = string.Format("/api/programs/get_all_programs/?callback=?");
            return await Get<TelevisionProgramScheduleMessage.Response>(query);
        }
    }
}
