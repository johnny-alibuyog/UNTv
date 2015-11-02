using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Splat;
using UNTv.WP81.Common.IO;
using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class WebStore : IStore
    {
        private readonly IBuilder _builder;
        private readonly ITextWriter _textWriter;

        public WebStore()
        {
            _textWriter = Locator.CurrentMutable.GetService<ITextWriter>();
            _builder = Locator.CurrentMutable.GetService<IBuilder>();
        }

        public async Task<TResponse> Get<TResponse>(IReturn<TResponse> request) where TResponse : class
        {
            try
            {
                var client = new HttpClient();
                var uri = _builder.BuildUri(request);
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var jsonResult = await response.Content.ReadAsStringAsync();
                if (jsonResult.StartsWith("?"))
                    jsonResult = jsonResult.Substring(2, jsonResult.Length - 3);

                //var filename = _builder.BuildFilename(request);
                //if (!string.IsNullOrWhiteSpace(filename))
                //    await _textWriter.Write(filename, jsonResult);

                return JsonConvert.DeserializeObject<TResponse>(jsonResult);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString()); // TODO: do some logging here
                return Activator.CreateInstance<TResponse>();
            }
        }
    }
}
