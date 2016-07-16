using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.IO;
using System.Windows.Media.Imaging;

namespace Song.Convert
{
    public class BinaryImageConverter : IValueConverter
    {    
        public object Convert( object value,Type targetType,object parameter,System.Globalization.CultureInfo culture )    
        {        
            if(value != null && value is byte[])        
            {
                byte[] bytes = value as byte[];
                MemoryStream stream = new MemoryStream( bytes );
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                return image;        
            }
            return null;    
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) 
        {
            throw new NotImplementedException();
        }
    }
}
