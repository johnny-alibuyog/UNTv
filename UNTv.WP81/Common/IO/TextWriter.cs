using System;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UNTv.WP81.Common.IO
{
    public class TextWriter : ITextWriter
    {
        private readonly StorageFolder _folder = ApplicationData.Current.LocalFolder;

        public async Task Write(string filename, string content)
        {
            if (System.Diagnostics.Debugger.IsAttached)
                return;

            var fileBytes = Encoding.UTF8.GetBytes(content.ToCharArray());
            var file = await _folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, content);
        }
    }
}
