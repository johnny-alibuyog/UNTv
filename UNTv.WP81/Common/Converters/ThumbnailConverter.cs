using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace UNTv.WP81.Common.Converters
{
    public class ThumbnailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value != null && value.ToString() != String.Empty)
                {
                    var uri = (Uri)null;
                    string url = value.ToString();
                    if (url.StartsWith("/"))
                    {
                        uri = new Uri(string.Concat("ms-appx://", url), UriKind.Absolute);
                    }
                    else
                    {
                        uri = new Uri(url, UriKind.Absolute);
                    }
                    var bm = new BitmapImage(uri)
                    {
                        CreateOptions = BitmapCreateOptions.None,
                        DecodePixelHeight = System.Convert.ToInt32(parameter)
                    };

                    return bm;
                }
            }
            catch (Exception ex)
            {
                //TODO: do some logging or something
                //AppLogs.WriteError("ThumbnailConverter.Convert", ex);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
