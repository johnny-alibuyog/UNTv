using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class SessionStore : IStore
    {
        private readonly IBuilder _builder;
        private static readonly IDictionary<string, string> _contianer = new Dictionary<string, string>();

        public SessionStore()
        {
            _builder = Locator.CurrentMutable.GetService<IBuilder>();
        }

        public async Task<TResponse> Get<TResponse>(Messages.IReturn<TResponse> request) where TResponse : class
        {
            throw new NotImplementedException();
        }

        public async Task<TResponse> Put<TResponse>(Messages.IReturn<TResponse> request, string data) where TResponse : class
        {
            throw new NotImplementedException();
        }
    }
}
