using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using UNTv.WP81.Common.IO;
using UNTv.WP81.Common.Extentions;
using Windows.Storage;

namespace UNTv.WP81.Data.Stores
{
    public class LocalDataStore : IDataStore
    {
        private readonly ITextWriter _textWriter;
        private readonly ITextReader _textReader;
        private readonly ApplicationDataContainer _container;


        public LocalDataStore()
        {
            _textWriter = Locator.CurrentMutable.GetService<ITextWriter>();
            _textReader = Locator.CurrentMutable.GetService<ITextReader>();
            _container = ApplicationData.Current.LocalSettings;
        }

        public async Task<bool> Exists(string filename)
        {
            return await filename.Exists();
        }

        public async Task<string> Get(string filename)
        {
            return await _textReader.Read(filename);
        }

        public async Task Save(string filename, string data)
        {
            await _textWriter.Write(filename, data);
            return;
        }
    }
}
