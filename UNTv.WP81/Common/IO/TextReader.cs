using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
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

        public async Task<string> Read(string filename, string path = null)
        {
            try
            {

                var root = Package.Current.InstalledLocation.Path;

                var folder = !string.IsNullOrEmpty(path)
                    ? await StorageFolder.GetFolderFromPathAsync(root + path)
                    : ApplicationData.Current.LocalFolder; //Package.Current.InstalledLocation; 

                var file = await folder.GetFileAsync(filename);

                var content = await FileIO.ReadTextAsync(file);

                return content;
            }
            catch(Exception ex)
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
