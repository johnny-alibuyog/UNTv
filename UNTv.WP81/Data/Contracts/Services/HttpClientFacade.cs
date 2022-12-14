using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class HttpClientFacade
    {
        public async Task<string> Get(string uri)
        {
            var cancellationSource = new CancellationTokenSource();
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(2);
                var response = await client.GetAsync(uri, cancellationSource.Token);
                //Debug.WriteLine(uri);
                //Debug.WriteLine(response.Headers);
                //Debug.WriteLine(response.Content);
                //Debug.WriteLine(response.RequestMessage);
                //response.EnsureSuccessStatusCode();

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(uri);
                    Debug.WriteLine(response.Headers);
                    Debug.WriteLine(response.Content);
                    Debug.WriteLine(response.RequestMessage);
                    return null;
                }

                var rawJsonResult = await response.Content.ReadAsStringAsync();
                var jsonResult = rawJsonResult.Trim();
                if (jsonResult.StartsWith("?"))
                    jsonResult = jsonResult.Substring(2, jsonResult.Length - 3);

                return jsonResult;
            }
            catch (WebException ex)
            {
                Debug.WriteLine("Web Exception: " + ex.ToString());
                return null;
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cancellationSource.Token)
                    Debug.WriteLine("Request Cancelled: " + ex.ToString()); // a real cancellation, triggered by the caller
                else
                    Debug.WriteLine("Request Timeout: " + ex.ToString());   // a web request timeout (possibly other things!?)
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("General Exception:" + ex != null ? ex.ToString() : string.Empty); // TODO: do some logging here
                return null;
            }
        }

        public async Task<string> Put(string uri, string data)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Post(string uri, string data)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Delete(string uri)
        {
            throw new NotImplementedException();
        }
    }
}
