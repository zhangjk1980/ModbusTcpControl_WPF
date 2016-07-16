using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace QureyUI.Convert
{
    public class dateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().Equals("0"))
            {
                return "当前日";
            }
            else if (value.ToString().Equals("1"))
            {
                return "上一日";
            }
            else
            {
                return "下一日";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
