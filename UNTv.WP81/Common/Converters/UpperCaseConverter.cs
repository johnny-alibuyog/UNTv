﻿using System;
using Windows.UI.Xaml.Data;

namespace UNTv.WP81.Common.Converters
{
    public class UpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                return ((string)value).ToUpper();
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
