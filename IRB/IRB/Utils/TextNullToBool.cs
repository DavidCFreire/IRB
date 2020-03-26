using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IRB.Utils
{
    public class TextNullToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool ret = true;
            string vs = (string)value;
            if (string.IsNullOrEmpty(vs))
                ret = false;
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
