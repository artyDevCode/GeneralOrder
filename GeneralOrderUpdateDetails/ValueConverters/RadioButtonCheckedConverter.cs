using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralOrderUpdateDetails
{
    public class RadioButtonCheckedConverter : BaseValueConverter<RadioButtonCheckedConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //switch (value)
            //    {
            //    case "1":
            //        break;
            //    case "2":
            //        break;
            //    case "3":
            //        break;
            //    case "4":
            //        break;
            //    default:
            //        break;
            //}
            if (value != null)
                return value.Equals(parameter);
            else
               return false; 
        }

        public override object ConvertBack(object value, Type targetType, object parameter,  System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToBoolean(value) ? parameter : null;
        }
    }

}
