using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace UNTv.WP81.Common.IO
{
    public class TextReader : ITextReader
    {
        public async Task<bool> FileExists(StorageFolder folder, string fileName)
        {
            var files = await folder.GetFilesAsync();
            return files.Any(x => x.Name == fileName);
        }

        public async Task<string> Read(string filename)
        {
            var content = (string)null;
            var local = ApplicationData.Current.LocalFolder;

            var exists = await FileExists(local, filename);
            if (!exists)
                return null;

            var stream = await local.OpenStreamForReadAsync(filename);
            using (var reader = new StreamReader(stream))
            {
                content = reader.ReadToEnd();
            }

            return content;
        }
    }
}
