using ModbusTcpControl_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Song.Convert
{
    public class String2CorlorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().Equals("000"))///低、正常、高
            {
                return System.ConsoleColor.Green;
            }
            else if (value.ToString().Equals("010"))///低、正常、高
            {
                return System.ConsoleColor.Yellow;
            }
            return System.ConsoleColor.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusEnum2CorlorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(GasPressureStatus.Lower))///低
            {
                //Color clr = Color.FromRgb(0, 255, 255);
                return new SolidColorBrush(Colors.Green);
            }
            else if (value.Equals(GasPressureStatus.Normal))///正常
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Status2CorlorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var t = value.ToString();
            if (t.Equals("0"))///低
            {
                //Color clr = Color.FromRgb(0, 255, 255);
                return new SolidColorBrush(Colors.Green);
            }
            else if (t.Equals("1"))///正常
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
