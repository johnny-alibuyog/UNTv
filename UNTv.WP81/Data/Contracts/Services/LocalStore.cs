using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Splat;
using UNTv.WP81.Common.IO;
using UNTv.WP81.Data.Contracts.Messages;
using Windows.Storage;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class LocalStore : IStore
    {
        private readonly IBuilder _builder;
        private readonly ApplicationDataContainer _container;

        public LocalStore()
        {
            _builder = Locator.CurrentMutable.GetService<IBuilder>();
            _container = ApplicationData.Current.LocalSettings;
        }


        public async Task<TResponse> Get<TResponse>(IReturn<TResponse> request) where TResponse : class
        {
            var response = Activator.CreateInstance<TResponse>();

            var key = _builder.BuildUri(request);
            if (!_container.Values.ContainsKey(key))
                return response;

            var jsonResult = _container.Values[key] as string;
            if (!string.IsNullOrWhiteSpace(jsonResult))
                response = JsonConvert.DeserializeObject<TResponse>(jsonResult);

            return response;
        }

        public async Task<TResponse> Put<TResponse>(IReturn<TResponse> request, string data) where TResponse : class
        {
            var key = _builder.BuildUri(request);
            _container.Values[key] = data;

            return Activator.CreateInstance<TResponse>();
        }

        //private readonly IBuilder _builder;
        //private readonly ITextReader _textReader;

        //public LocalStore()
        //{
        //    _builder = Locator.CurrentMutable.GetService<IBuilder>();
        //    _textReader = Locator.CurrentMutable.GetService<ITextReader>();
        //}

        //public async Task<TResponse> Get<TResponse>(IReturn<TResponse> request) where TResponse : class
        //{
        //    try
        //    {
        //        var filename = _builder.BuildFilename(request);
        //        var jsonResult = await _textReader.Read(filename);
        //        var response = Activator.CreateInstance<TResponse>();

        //        if (!string.IsNullOrWhiteSpace(jsonResult))
        //            response = JsonConvert.DeserializeObject<TResponse>(jsonResult);

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString()); // TODO: do some logging here
        //        return Activator.CreateInstance<TResponse>();
        //    }
        //}

        //public Task<TResponse> Put<TResponse>(IReturn<TResponse> request, object data) where TResponse : class
        //{
        //    throw new NotImplementedException();
        //}
    }
}
