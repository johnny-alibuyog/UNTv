using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Splat;
using UNTv.WP81.Common.IO;
using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class LocalStore : IStore
    {
        private readonly IBuilder _builder;
        private readonly ITextReader _textReader;

        public LocalStore()
        {
            _builder = Locator.CurrentMutable.GetService<IBuilder>();
            _textReader = Locator.CurrentMutable.GetService<ITextReader>();
        }

        public async Task<TResponse> Get<TResponse>(IReturn<TResponse> request) where TResponse : class
        {
            var filename = _builder.BuildFilename(request);
            var jsonResult = await _textReader.Read(filename);
            var response = Activator.CreateInstance<TResponse>();

            if (!string.IsNullOrWhiteSpace(jsonResult))
                response = JsonConvert.DeserializeObject<TResponse>(jsonResult);

            return response;
        }
    }
}
