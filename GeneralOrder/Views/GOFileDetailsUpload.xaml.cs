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

namespace GeneralOrder
{
    /// <summary>
    /// Interaction logic for GOFileDetailsUpload.xaml
    /// </summary>
    public partial class GOFileDetailsUpload : UserControl
    {
        public GOFileDetailsUpload()
        {
            InitializeComponent();
            var GOfileUVM = new GOFileDetailsUploadVM();
            GOfileUVM.detailsUploadRichTextBox = detailsUploadRichTextBox;
            this.DataContext = GOfileUVM;
        }
    }
}
