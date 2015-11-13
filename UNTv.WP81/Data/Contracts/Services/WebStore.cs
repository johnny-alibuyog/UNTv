using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
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
            _builder = Locator.CurrentMutable.GetService<IBuilder>();
            _textWriter = Locator.CurrentMutable.GetService<ITextWriter>();
        }

        public async Task<TResponse> Get<TResponse>(IReturn<TResponse> request) where TResponse : class
        {
            var cancellationSource = new CancellationTokenSource();
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(2);
                var uri = _builder.BuildUri(request);
                var response = await client.GetAsync(uri, cancellationSource.Token);
                response.EnsureSuccessStatusCode();

                var jsonResult = await response.Content.ReadAsStringAsync();
                if (jsonResult.StartsWith("?"))
                    jsonResult = jsonResult.Substring(2, jsonResult.Length - 3);

                // make sure json string is serializable before saving it to local storage
                var result = JsonConvert.DeserializeObject<TResponse>(jsonResult);

                // save to local storage
                var filename = _builder.BuildFilename(request);
                if (!string.IsNullOrWhiteSpace(filename))
                    await _textWriter.Write(filename, jsonResult);

                return result;
            }
            catch (WebException ex)
            {
                Debug.WriteLine("Web Exception: " + ex.ToString()); // TODO: do some logging here
                return Activator.CreateInstance<TResponse>();
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cancellationSource.Token)
                {
                    // a real cancellation, triggered by the caller
                    Debug.WriteLine("Request Cancelled: " + ex.ToString()); // TODO: do some logging here
                    return Activator.CreateInstance<TResponse>();
                }
                else
                {
                    // a web request timeout (possibly other things!?)
                    Debug.WriteLine("Request Timeout: " + ex.ToString()); // TODO: do some logging here
                    return Activator.CreateInstance<TResponse>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("General Exception:" + ex != null ? ex.ToString() : string.Empty); // TODO: do some logging here
                return Activator.CreateInstance<TResponse>();
            }
        }


        public Task<TResponse> Put<TResponse>(IReturn<TResponse> request, string data) where TResponse : class
        {
            throw new NotImplementedException();
        }
    }
}
