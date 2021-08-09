using System.Windows;

namespace GeneralOrder
{
    /// <summary>
    /// Interaction logic for GOImportFileSelectAct.xaml
    /// </summary>
    public partial class GOImportFileSelectAct : Window
    {
        public GOImportFileSelectAct()
        {
            InitializeComponent();
            this.DataContext = GOImportFileSelectActVM.instance = new GOImportFileSelectActVM(this);
        }
    }
}
