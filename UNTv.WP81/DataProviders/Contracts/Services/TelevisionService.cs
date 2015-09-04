using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UNTv.WP81.DataProviders.Contracts.Messages;

namespace UNTv.WP81.DataProviders.Contracts.Services
{
    public class TelevisionService : ServiceBase, ITelevisionService //: BaseRestService, IWebTvService
    {
        private readonly int _count = 20;

        public TelevisionService()
        {
            this.BaseAddress = new Uri("http://www.untvweb.com");
        }

        public async Task<NewsResponse> Get(NewsRequest request)
        {
            var query = string.Format("/news/api/get_category_posts/?callback=?&count={0}&slug={1}", request.Count ?? _count, request.Category);
            return await Get<NewsResponse>(query);
        }

        public async Task<VideosResponse> Get(VideosRequest request)
        {
            var query = string.Format("/api/programs/get_sorted_video_list/?sortby={0}&numposts={1}", request.SortBy.ToString().ToLower(), request.Count ?? _count);
            return await Get<VideosResponse>(query);
        }

        public async Task<PublicServicesResponse> Get(PublicServicesRequest request)
        {
            var query = string.Format("/api/programs/get_page_children_shallow/?slug=advocacies&callback=?");
            return await Get<PublicServicesResponse>(query);
        }

        public async Task<TelevisionProgramsResponse> Get(TelevisionProgramsRequest request)
        {
            var query = string.Format("/api/programs/get_programs/?callback=?");
            return await Get<TelevisionProgramsResponse>(query);
        }

        public async Task<TelevisionProgramSchedulesResponse> Get(TelevisionProgramSchedulesRequest request)
        {
            var query = string.Format("/api/programs/get_all_programs/?callback=?");
            return await Get<TelevisionProgramSchedulesResponse>(query);
        }
    }
}
