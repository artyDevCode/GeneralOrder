using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace GeneralOrderUpdateDetails
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow(int ildNumber)
        {
           //Set globals
            string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            System.Reflection.Assembly.LoadFile(exePath + "\\System.Windows.Interactivity.dll");
            System.Reflection.Assembly.LoadFile(exePath + "\\FontAwesome.WPF.dll");

            //set culture.
            var culture = new CultureInfo("en-AU");
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));

            InitializeComponent();

            //Set the static instance variables
            this.DataContext = WindowViewModel.instance = new WindowViewModel(this);
            WindowViewModel.instance.loggerFlowDoc = new FlowDocument();
            WindowViewModel.instance.loggerFlowDoc.LoggerFlowDocParser($"{DateTime.Now} --- Started..");
            WindowViewModel.instance.selectedRecord = new GeneralOrderCore.GeneralFieldsViewlModel();
            WindowViewModel.instance.selectedRecord.ildNumber = ildNumber;
           
        }

    }
}
