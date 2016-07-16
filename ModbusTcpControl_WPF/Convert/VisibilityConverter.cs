using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Song.Convert
{
    public class Bool2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().ToLower().Equals("true"))
            {
                return System.Windows.Visibility.Visible;
            }
            return System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().Equals("Visible"))
            {
                return true;
            }
            return false;
        }
    }

    public class Str2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().ToLower().Equals("-"))
            {
                return System.Windows.Visibility.Visible;
            }
            return System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().Equals("Visible"))
            {
                return "-";
            }
            return "+";
        }
    }

    public class Visibility2BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().Equals("Visible"))
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            if (value.ToString().ToLower().Equals("true"))
            {
                return System.Windows.Visibility.Visible;
            }
            return System.Windows.Visibility.Collapsed;
        }
    }
    public class VisibilityConverterBy2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter.ToString() == "刷卡")
            {
                if (value.ToString() == "0")
                {
                    return System.Windows.Visibility.Collapsed;
                }
                else
                {
                    return System.Windows.Visibility.Visible;
                }
            }
            else if (parameter.ToString() == "常开")
            {
                if (value.ToString() == "1")
                {
                    return System.Windows.Visibility.Collapsed;
                }
                else
                {
                    return System.Windows.Visibility.Visible;
                }
            }
            else
            {
                if (value.ToString() == "2")
                {
                    return System.Windows.Visibility.Collapsed;
                }
                else
                {
                    return System.Windows.Visibility.Visible;
                }
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
