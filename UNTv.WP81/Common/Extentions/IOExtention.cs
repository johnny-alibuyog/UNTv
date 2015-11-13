using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace UNTv.WP81.Common.Extentions
{
    public static class IOExtention
    {
        public async static Task<bool> HasFile(this StorageFolder folder, string fileName)
        {
            var files = await folder.GetFilesAsync();
            return files.Any(x => x.Name == fileName);
        }

        public async static Task<bool> Exists(this string filename, string path = null)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                var folder = await StorageFolder.GetFolderFromPathAsync(path);
                if (await folder.HasFile(filename))
                    return true;
            }

            if (await ApplicationData.Current.LocalFolder.HasFile(filename))
                return true;

            if (await Package.Current.InstalledLocation.HasFile(filename))
                return true;

            return false;
        }
    }
}
