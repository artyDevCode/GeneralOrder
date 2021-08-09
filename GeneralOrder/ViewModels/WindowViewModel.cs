using DatabaseEntity;
using GeneralOrderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace GeneralOrder
{

    /// <summary>
    /// The Class that is linked to the MainViewModel
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region properties
        /// <summary>
        /// The window this view model controls
        /// </summary>
        public System.Windows.Window mWindow;

        private FlowDocument _loggerFlowDoc { get; set; }
        public FlowDocument loggerFlowDoc
        {
            get { return _loggerFlowDoc; }
            set
            {
                _loggerFlowDoc = value;
                OnPropertyChanged("loggerFlowDoc");
            }
        }


        /// <summary>
        /// The current page of the application
        /// </summary>
        private ApplicationPage _CurrentPage { get; set; } //=   ApplicationPage.goImportFile;  //ApplicationPage.goFileDetailsUpload; //
        public ApplicationPage CurrentPage
        {
            get { return _CurrentPage; }
            set
            {
                _CurrentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 200;

        public int _UIFontSize { get; set; }
        public int UIFontSize
        {
            get { return _UIFontSize; }
            set
            {
                _UIFontSize = value;
                OnPropertyChanged("UIFontSize");
            }
        }
        #endregion

        #region UISettings
        public int UIWidth { get; set; }
        public int GOImportFileHeight { get; set; }
        public int GOFileDetailsUploadHeight { get; set; }
        public int GODepartmentForActHeight { get; set; }
        public int UILoggerHeight { get; set; }
        public int UILoggerRTBHeight { get; set; }
        public int UILoggerWidth { get; set; }
        public string generalOrderPadding { get; set; }
        public string generalOrderMargin { get; set; }
        #endregion

        public static WindowViewModel instance;
       // public List<GeneralOrderMetadataViewModel> resMetaList = new List<GeneralOrderMetadataViewModel>();
        public GeneralOrderMetadataViewModel resMeta = new GeneralOrderMetadataViewModel();
        public int MinFont { get; set; }
        public int MaxFont { get; set; }
        #region Commands
    
        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion


        /// <summary>
        /// Conastructor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            //Get the Application window fornt size from the GeneralOrderCore config file
            var retObj = GeneralOrderCore.AppConfig.GetAppSettings();

            //Set the global variables
            MinFont = Convert.ToInt16(retObj[0]);
            UIFontSize = Convert.ToInt16(retObj[1]);
            MaxFont = Convert.ToInt16(retObj[2]);
            UIFontSize = UIFontSize > MaxFont ? MaxFont : UIFontSize;
            UIFontSize = UIFontSize < MinFont ? MinFont : UIFontSize;
            //set the padding and margin size based on font size
            generalOrderPadding = (UIFontSize - 4).ToString(); // (UIFontSize / 10) + UIFontSize;
            generalOrderMargin = UIFontSize + " 10 " + UIFontSize + " 10";
            UIWidth = (800 * Convert.ToInt32(UIFontSize + "0")) / 110;
            //Set the window height and widtgh
            GOImportFileHeight = (320 * Convert.ToInt32(UIFontSize + "0")) / 110;
            GOFileDetailsUploadHeight = (625 * Convert.ToInt32(UIFontSize + "0")) / 110;
            GODepartmentForActHeight = (625 * Convert.ToInt32(UIFontSize + "0")) / 110;
            //Set the application logger and RichTexBox height and width
            UILoggerWidth = (800 * Convert.ToInt32(UIFontSize + "0")) / 110;
            UILoggerHeight = (100 * Convert.ToInt32(UIFontSize + "0")) / 110;
            UILoggerRTBHeight = (75 * Convert.ToInt32(UIFontSize + "0")) / 110;

            mWindow = window;
            loggerFlowDoc = new FlowDocument();

            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);

            CloseCommand = new RelayCommand(WindowClose);
       
            CurrentPage = ApplicationPage.goImportFile;

        }

       //Global method to close the application
        public void WindowClose()
        {
            mWindow.Close();
        }


    }
}
