using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    public class ScreenHelpViewModel
    {
        public int ScreenHelpID { get; set; }
        public string ScreenName { get; set; }
        public string ScreenHelpText { get; set; }
        public  List<ControlHelperViewModel> ControlHelperVM { get; set; }
    }
    public class ControlHelperViewModel
    {
        public string ControlName { get; set; }
        public string ControlHelpText { get; set; }
    }
}
