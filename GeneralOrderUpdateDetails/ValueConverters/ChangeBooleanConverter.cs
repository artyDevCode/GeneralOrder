using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralOrderUpdateDetails
{
    public class ChangeBooleanConverter : BaseValueConverter<ChangeBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
            switch (value)
            {
                case true:
                    return false;
                case false:
                    return true;
             
                default:
                    return false;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
