using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UNTv.WP81.DataProviders.Contracts.Services
{
    public class ServiceBase
    {
        public Uri BaseAddress {get;set;}

        public async Task<T> Get<T>(string queryString)
        {
            var client = new HttpClient();
            client.BaseAddress = BaseAddress;

            var response = await client.GetAsync(queryString);
            response.EnsureSuccessStatusCode();

            var jsonResult = await response.Content.ReadAsStringAsync();
            if (jsonResult.StartsWith("?"))
                jsonResult = jsonResult.Substring(2, jsonResult.Length - 3);

            return JsonConvert.DeserializeObject<T>(jsonResult);
        }
    }
}
