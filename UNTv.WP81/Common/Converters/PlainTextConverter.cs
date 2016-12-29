using System;
using UNTv.WP81.Common.Extentions;
using Windows.UI.Xaml.Data;

namespace UNTv.WP81.Common.Converters
{
    public class PlainTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var plainText = String.Empty;
            try
            {
                if (value != null)
                {
                    var text = value.ToString();
                    if (text.Length > 0)
                    {
                        plainText = text.DecodeHtml();
                        if (parameter != null)
                        {
                            var maxLength = 0;
                            Int32.TryParse(parameter.ToString(), out maxLength);
                            if (maxLength > 0)
                            {
                                plainText = plainText.Truncate(maxLength);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Log here or something
                //AppLogs.WriteError("PlainTextConverter.Convert", ex);
            }
            return plainText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
