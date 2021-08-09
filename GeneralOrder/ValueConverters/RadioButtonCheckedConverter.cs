using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralOrder
{
    public class RadioButtonCheckedConverter : BaseValueConverter<RadioButtonCheckedConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value != null)
                return value.Equals(parameter);
            else
                return false;
            //switch (value)
            //{
            //    case "YES":
            //        return true;
            //    case "NO":
            //        return false;
            //    default:
            //        return false;
            //}
        }

        public override object ConvertBack(object value, Type targetType, object parameter,  System.Globalization.CultureInfo culture)
        {       
            return System.Convert.ToBoolean(value) ? parameter : null;
        }
    }

}
