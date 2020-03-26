using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IRB.Utils
{
    public class SelectedToFontAttributeConverter : IValueConverter
    {

        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((Boolean)value)
                    return FontAttributes.Bold;
                else
                    return FontAttributes.None;
            }
            return FontAttributes.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is FontAttributes)
            {
                if ((FontAttributes)value == FontAttributes.Bold)
                    return true;
                else
                    return false;
            }
            return false;
        }

        #endregion
    }
}
