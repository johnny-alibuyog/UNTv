using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UNTv.WP81.Common.IO
{
    public class TextWriter : ITextWriter
    {
        public async Task Write(string filename, string content)
        {
            var fileBytes = Encoding.UTF8.GetBytes(content.ToCharArray());
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                stream.Write(fileBytes, 0, fileBytes.Length);
            }
        }
    }
}
