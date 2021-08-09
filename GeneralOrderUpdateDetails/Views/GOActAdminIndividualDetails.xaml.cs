using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeneralOrderUpdateDetails
{
    /// <summary>
    /// Interaction logic for GOFileDetailsUpload.xaml
    /// </summary>
    public partial class GOActAdminIndividualDetails : UserControl
    {
        public GOActAdminIndividualDetails()
        {
            InitializeComponent();
            var GOfileIVM = new GOActAdminIndividualDetailsVM();
            GOfileIVM.individualUploadRichTextBox = individualUploadRichTextBox;
            this.DataContext = GOfileIVM;
           // this.DataContext = new GOActAdminIndividualDetailsVM();
        }
    }
}
