using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UNTv.WP81.Common.Extentions;
using Windows.Storage;

namespace UNTv.WP81.Common.IO
{
    public class TextReader : ITextReader
    {
        public async Task<string> Read(string filename, string path = null)
        {
            try
            {
                var folder = !string.IsNullOrEmpty(path)
                    ? await StorageFolder.GetFolderFromPathAsync(path)
                    : ApplicationData.Current.LocalFolder; 

                if (!await folder.HasFile(filename))
                    return null;

                var file = await folder.GetFileAsync(filename);
                var content = await FileIO.ReadTextAsync(file);

                return content;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString()); // TODO: Log error
                return null;
            }

            //var content = (string)null;
            //var folder = ApplicationData.Current.LocalFolder;

            //var exists = await FileExists(folder, filename);
            //if (!exists)
            //    return null;

            //var stream = await folder.OpenStreamForReadAsync(filename);
            //using (var reader = new StreamReader(stream))
            //{
            //    content = reader.ReadToEnd();
            //}

            //return content;
        }
    }
}
