using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Song.Convert
{
    public class String2SolidColorBrushConverter : IValueConverter
    {    
        public object Convert( object value,Type targetType,object parameter,System.Globalization.CultureInfo culture )    
        {        
            if(value != null && value is string)        
            {
                string colorName = value as string;
                Color color = (Color)ColorConverter.ConvertFromString(colorName);
                return new SolidColorBrush(color); 
            }
            return null;    
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) 
        {
            throw new NotImplementedException();
        }
    }
}
