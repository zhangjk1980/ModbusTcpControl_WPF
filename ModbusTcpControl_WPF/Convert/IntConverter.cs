using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace QureyUI.Convert
{
    public class IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter.ToString().Equals("stationtype"))
            {
                if (value.ToString() == "0")
                {
                    return "入口";
                }
                else
                {
                    return "出口";
                }
            }
            else if (parameter.ToString().Equals("state"))
            {
                if (value.ToString() == "0")
                {
                    return "正常";
                }
                else
                {
                    return "故障";
                }
            }
            else
            {
                if (value.ToString() == "0")
                {
                    return "刷卡";
                }
                else if (value.ToString() == "1")
                {
                    return "常开";
                }
                else
                {
                    return "常闭";
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
