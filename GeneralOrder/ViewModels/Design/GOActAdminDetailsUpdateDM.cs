using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralOrder
{
    public class GOActAdminDetailsUpdateDM : BaseViewModel
    {
        public static GOActAdminDetailsUpdateDM Instance => new GOActAdminDetailsUpdateDM();
        public ObservableCollection<object> listBox { get; set; }
        public string textMessage { get; set; } = "Are you sure you want to continue?";
        public string BtnNoQuit { get; set; } = "NO";

        public GOActAdminDetailsUpdateDM()
        {
            listBox = new ObservableCollection<object>();

            listBox.Add(new OrigAdminDetails
            {
                portfolio = "Portfolio",
                department = "Department",
                comments = "Comments",
                effectiveDate = "Effective Date",
                change = "Change",
                backgroundColour = "Gray",
                foregroundColour = "White"
            });
            listBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for public transport",
                department = "DEDJR",
                comments = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec elementum, neque vel tincidunt mattis, enim nisl ullamcorper tellus, a vulputate turpis augue quis diam.",
                effectiveDate = "12/10/2017",
                change = "M1",
                backgroundColour = "white",
                foregroundColour = "LightBlue"
            });
            listBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for roads and safety",
                department = "DEDJR",
                comments = "Donec vitae sollicitudin ipsum. In orci dui, accumsan at magna eu, efficitur tincidunt ex. Maecenas quis purus et orci maximus eleifend.",
                effectiveDate = "11/09/2017",
                change = "M1",
                backgroundColour = "LightGray",
                foregroundColour = "White"
            });
            listBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for parks",
                department = "DPC",
                comments = "Maecenas interdum, odio vel rutrum bibendum, augue libero mattis sem, ut vulputate dui libero ut risus. Proin posuere vulputate orci non dictum.",
                effectiveDate = "05/05/2017",
                change = "M1",
                backgroundColour = "white",
                foregroundColour = "LightBlue"
            });
            listBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for Major projects",
                department = "DET",
                comments = "Aenean volutpat iaculis lorem vel venenatis. Sed sodales elit non neque eleifend, in venenatis ligula pharetra.",
                effectiveDate = "12/03/2017",
                change = "D1",
                backgroundColour = "LightGray",
                foregroundColour = "White"
            });
            listBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for public transport",
                department = "DTF",
                comments = "Aliquam vehicula hendrerit diam pretium hendrerit. Ut in ipsum non massa consequat convallis. Ut ultricies dui quis gravida mattis.",
                effectiveDate = "22/02/2017",
                change = "D1",
                backgroundColour = "white",
                foregroundColour = "LightBlue"
            });
            //******************************************

            listBox.Add(new NewAdminDetails
            {
                portfolio = "Portfolio",
                department = "Department",
                comments = "Comments",
                effectiveDate = "Effective Date",
                change = "Change",
                backgroundColour = "Gray",
                foregroundColour = "White"
            });
            listBox.Add(new NewAdminDetails
            {
                portfolio = "Minister for public transport",
                department = "DEDJR",
                comments = "Aliquam vehicula hendrerit diam pretium hendrerit. Ut in ipsum non massa consequat convallis. Ut ultricies dui quis gravida mattis.",
                effectiveDate = "22/02/2017",
                change = "A1",
                backgroundColour = "white",
                foregroundColour = "LightGreen"
            });
            listBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for public transport",
                department = "DPC",
                comments = "Aliquam vehicula hendrerit diam pretium hendrerit. Ut in ipsum non massa consequat convallis. Ut ultricies dui quis gravida mattis.",
                effectiveDate = "22/02/2017",
                change = "M1",
                backgroundColour = "LightGray",
                foregroundColour = "White"
            });
            listBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for public transport",
                department = "DOJ",
                comments = "Aliquam vehicula hendrerit diam pretium hendrerit. Ut in ipsum non massa consequat convallis. Ut ultricies dui quis gravida mattis.",
                effectiveDate = "22/02/2017",
                change = "D1",
                backgroundColour = "white",
                foregroundColour = "LightGreen"
            });
            listBox.Add(new OrigAdminDetails
            {
                portfolio = "Minister for public transport",
                department = "DET",
                comments = "Aliquam vehicula hendrerit diam pretium hendrerit. Ut in ipsum non massa consequat convallis. Ut ultricies dui quis gravida mattis.",
                effectiveDate = "22/02/2017",
                change = "A1",
                backgroundColour = "LightGray",
                foregroundColour = "White"
            });
        }
    }
}
