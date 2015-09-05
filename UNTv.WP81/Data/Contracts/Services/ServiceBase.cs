using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UNTv.WP81.Common.IO;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class ServiceBase
    {
        //private readonly ITextWriter _textWriter;

        public Uri BaseAddress {get;set;}

        public async Task<TResponse> Get<TResponse>(string queryString, string localPersistenceFilename = null)
        {
            var client = new HttpClient();
            client.BaseAddress = BaseAddress;

            var response = await client.GetAsync(queryString);
            response.EnsureSuccessStatusCode();

            var jsonResult = await response.Content.ReadAsStringAsync();
            if (jsonResult.StartsWith("?"))
                jsonResult = jsonResult.Substring(2, jsonResult.Length - 3);

            //if (!string.IsNullOrWhiteSpace(localPersistenceFilename))
            //    await _textWriter.Write(localPersistenceFilename, jsonResult);

            return JsonConvert.DeserializeObject<TResponse>(jsonResult);
        }
    }
}
