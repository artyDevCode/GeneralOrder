using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralOrderCore
{
    public class PendingEditTypeHelper
    {
        public static string GetPendingEditTypeText(short pendingEditType)
        {
            //None = 0,
            //Add = 1,
            //Modify = 2,
            //Delete = 3
            switch (pendingEditType)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return "A";
                case 2:
                    return "M";
                case 3:
                    return "D";
                default:
                    return string.Empty;
            }
        }
    }
}
