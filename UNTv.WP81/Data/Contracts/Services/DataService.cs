using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Splat;
using UNTv.WP81.Common.Extentions;
using UNTv.WP81.Data.Contracts.Messages;
using UNTv.WP81.Data.Stores;
using Windows.Networking.Connectivity;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class DataService : IDataService
    {
        private readonly IBuilder _builder;
        private readonly IDataStore _localData;
        private readonly IDataStore _sessionData;
        private readonly HttpClientFacade _web;

        public DataService()
        {
            _web = Locator.CurrentMutable.GetService<HttpClientFacade>();
            _localData = Locator.CurrentMutable.GetService<LocalDataStore>();
            _sessionData = Locator.CurrentMutable.GetService<SessionDataStore>();
            _builder = Locator.CurrentMutable.GetService<IBuilder>();

        }

        public async Task<TResponse> Get<TResponse>(IReturn<TResponse> request) where TResponse : class
        {
            try
            {
                var json = (string)null;
                var uri = _builder.BuildUri(request);
                var filename = _builder.BuildFilename(request);

                if (await _sessionData.Exists(filename))
                {
                    json = await _sessionData.Get(filename);
                }
                else
                {
                    if (NetworkExtention.HasInternetConnection())
                    {
                        json = await _web.Get(uri);
                        if (!string.IsNullOrWhiteSpace(json))
                        {
                            JContainer.Parse(json); // this will ensure if the json is valid
                            await _localData.Save(filename, json);
                            await _sessionData.Save(filename, json);
                        }
                    }
                    else
                    {
                        if (await _localData.Exists(filename))
                            json = await _localData.Get(filename);
                    }
                }

                return !string.IsNullOrWhiteSpace(json)
                    ? JsonConvert.DeserializeObject<TResponse>(json)
                    : Activator.CreateInstance<TResponse>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Parse Exception: " + ex.ToString());
                return Activator.CreateInstance<TResponse>();
            }
        }
    }
}
