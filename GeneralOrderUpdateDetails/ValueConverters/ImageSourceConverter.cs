using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralOrderUpdateDetails
{
    public class ImageSourceConverter : BaseValueConverter<ImageSourceConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //// Find the appropriate page
            switch (value)
            {
                case true:
                    return "Visible";
                case false:
                    return "Hidden";
                default:                  
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
