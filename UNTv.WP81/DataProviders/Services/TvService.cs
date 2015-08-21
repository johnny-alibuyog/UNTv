using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UNTv.WP81.DataProviders.Models;

namespace UNTv.WP81.DataProviders.Services
{
    public class TvService
    {
        private readonly Uri _baseAddress = new Uri("http://www.untvweb.com/");

        public async Task<T> Get<T>(string queryString)
        {
            var client = new HttpClient();
            client.BaseAddress = _baseAddress;

            var response = await client.GetAsync(queryString);
            response.EnsureSuccessStatusCode();

            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResult);


            //using (Stream s = client.GetStreamAsync("http://www.test.com/large.json").Result)
            //using (StreamReader sr = new StreamReader(s))
            //using (JsonReader reader = new JsonTextReader(sr))
            //{
            //JsonSerializer serializer = new JsonSerializer();
 
            //// read the json from a stream
            //// json size doesn't matter because only a small piece is read at a time from the HTTP request
            //Person p = serializer.Deserialize<Person>(reader);
            //}
        }

        //public async Task<List<Post>> News(Category category)
        //{
        //    var queryString = string.Format("news/api/get_category_posts/?callback=?&count=12&slug={0}", category.Slug);
            
        //}
    }
}
