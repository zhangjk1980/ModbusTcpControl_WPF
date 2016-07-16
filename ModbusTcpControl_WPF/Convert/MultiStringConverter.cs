using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace QureyUI.Convert
{
    public class MultiStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string state = string.Empty;
            if (values[2].ToString().Equals("0"))
            {
                state = "刷卡";
            }
            else if (values[2].ToString().Equals("1"))
            {
                state = "常开";
            }
            else
            {
                state = "常闭";
            }
            return string.Format("{0}-{1}   {2}", values[0].ToString(), values[1].ToString(), state);
        } 

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
