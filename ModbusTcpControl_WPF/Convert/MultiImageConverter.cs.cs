using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows;

namespace QureyUI.Convert
{
   public class MultiImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0].ToString() == "0")//是入口
            {
                if (values[1].ToString() == "1")//正常
                {
                    //return cutImage(@"pack://application:,,,/PicResource/入_正常2.png", 0, 0, 150, 107);
                    return BitmapFrame.Create(new Uri("pack://application:,,,/PicResource/入_正常3.png", UriKind.RelativeOrAbsolute));
                }
                else//故障
                {
                    //return cutImage(@"pack://application:,,,/PicResource/入_正常2.png", 0, 0, 150, 107);
                    return BitmapFrame.Create(new Uri("pack://application:,,,/PicResource/入_报警3.png", UriKind.RelativeOrAbsolute));
                }
            }
            else//出口
            {
                if (values[1].ToString() == "1")//正常
                {
                    return BitmapFrame.Create(new Uri("pack://application:,,,/PicResource/出_正常3.png", UriKind.RelativeOrAbsolute));
                }
                else//故障
                {
                    return BitmapFrame.Create(new Uri("pack://application:,,,/PicResource/出_报警3.png", UriKind.RelativeOrAbsolute));
                }
            } 
        }
        //private BitmapSource cutImage(string imgaddress, int x, int y, int width, int height)
        //{ 
        //    return new CroppedBitmap( 
        //        BitmapFrame.Create(new Uri(imgaddress, UriKind.Relative)), 
        //        new Int32Rect(x, y, width, height) 
        //         );
        //}
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
