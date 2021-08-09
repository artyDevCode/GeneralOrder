using System.Windows.Controls;


namespace GeneralOrderUpdateDetails
{
    /// <summary>
    /// Interaction logic for GOActAdminDetailsUpdate.xaml
    /// </summary>
    public partial class GOActAdminDetailsUpdate : UserControl
    {
        public GOActAdminDetailsUpdate()
        {
            InitializeComponent();

            this.DataContext = GOActAdminDetailsUpdateVM.instance = new GOActAdminDetailsUpdateVM();
            GOActAdminDetailsUpdateVM.instance.GetAllGORecordsByILD();
        }
    }
}
