using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.DataProviders.Services
{
    public class WebTvService : IWebTvService //: BaseRestService, IWebTvService
    {
        private readonly int _pageItem = 20;

        private readonly Uri _baseAddress = new Uri("http://www.untvweb.com");

        public async Task<T> Get<T>(string queryString)
        {
            var client = new HttpClient();
            client.BaseAddress = _baseAddress;

            var response = await client.GetAsync(queryString);
            response.EnsureSuccessStatusCode();

            var jsonResult = await response.Content.ReadAsStringAsync();
            if (jsonResult.StartsWith("?"))
                jsonResult = jsonResult.Substring(2, jsonResult.Length - 3);

            return JsonConvert.DeserializeObject<T>(jsonResult);
        }

        public async Task<NewsResponse> Get(NewsRequest request)
        {
            var query = string.Format("/news/api/get_category_posts/?callback=?&count={0}&slug={1}", _pageItem, request.Category);
            return await Get<NewsResponse>(query);
        }

        public async Task<TvProgramResponse> Get(TvProgramRequest request)
        {
            var query = string.Format("http://www.untvweb.com/api/programs/get_sorted_video_list/?sortby={0}&numposts={1}", request.SortBy.ToString().ToLower(), _pageItem);
            return await Get<TvProgramResponse>(query);
        }
    }
}
