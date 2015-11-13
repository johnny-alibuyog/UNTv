using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.Data.Stores
{
    public class SessionDataStore : IDataStore
    {
        private static readonly IDictionary<string, string> _container = new Dictionary<string, string>();

        public async Task<bool> Exists(string filename)
        {
            return _container.ContainsKey(filename);
        }

        public async Task<string> Get(string filename)
        {
            if (!await Exists(filename))
                return null;

            return _container[filename] as string;
        }

        public async Task Save(string filename, string data)
        {
            _container[filename] = data;
            return;
        }
    }
}
